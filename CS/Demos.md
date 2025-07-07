# Effectief C# programmeren - Demos

## Demo 1: Onderscheid tussen Structures en classes

- Laat code zien en vraag wat er geschreven gaat worden.

- Pas de Move methode aan naar `ref` en vraag wat het antwoord gaat worden.

- Haal de `ref` weg en pas de `struct` aan naar `class` en vraag wat het antwoord gaat worden.

- Pas de Move methode aan en voeg onder aan de methode `p = new Point() { X = 6 };` toen en vraag wat het antwoord gaat worden.

- Pas de Move methode aan naar `ref` en vraag wat het antwoord gaat worden.

- Haal nog de `ref` weg. Pas de Move methode nog 1x aan naar `struct` en vraag wat het antwoord gaat worden.

- Vervang de inhoud van de move functie door `p = null;` en maak de declaratie `private static void Move(Point? p)` en vraag wat het antwoord gaat worden.

- Pas de Move methode aan naar `ref`, de `var p` naar `Point? p` en `$"X: {p?.X}, Y: {p?.Y}"` en vraag wat het antwoord gaat worden.

## Demo 2a: Fields en thread safety

- Ze de code op release build en draai de code met CRTL-F5 om te zorgen dat er geen debugger aan hangt.

- We bouwen een simpele methode die een teller bij houd hoe vaak deze gebruiker de methode heeft aangeroepen.

  - Toon StaticVar. Laat zien dat de `this.` hier niet beschikbaar is.

  - Run methode, de code werkt

- We gaan nu deze methode aanroepen in een multi-thread omgeving, dit kan zijn de webserver, profitserver maar ook de afasman als er een backgroundworker draait.

- Pas de RunMe aan zodat de code onder commentaar wordt uitgevoerd.

- `Runner` heeft inconsistent gedrag. Dit komt omdat een shared methode state gebruikt, maar niet thread safe geprogrammeerd is.

## Demo 2b: Fields en thread safety

-   Stel ik ben gebruiker (en niet bouwer) van de `PrintCalls` methode. Maar deze library wordt gebruikt door diverse partijen. Hoe weet ik nu dat een andere partij de `PrintCalls` methode op een andere thread aanroept? dat kan ik niet weten...

- Vervang de `PrintCalls` methode door de onderstaande code:

  ```csharp
  private static void AddAndPrintValueWithModule()
  {
    var x = new ClassVar();
    x.PrintCalls();
    x.PrintCalls();
    x.PrintCalls();
  }
  ```
- Run de code. Er is nu verwacht gedrag, maar geen teller over de gehele applicatie heen.

- Laat zien dat in `PrintCalls` op de class `this.` wel beschikbaar is.

- Verplaats `dim x as new ClassVar()` naar module niveau. Het onverwacht gedrag komt nu weer terug. Echter nu heb ik er zelf voor gekozen de `x` te delen over threads.

## Demo 3a: Constructors vs Construction methods

- Laat de `Settings` class zien en vraag wat je hier anders zou doen.

- Het in lezen van een bestand is potentieel zwaar en heeft een redelijke kans dat het tot exceptions leidt. Beide goede redenen om dit niet in een constructor te plaatsen. Laten we die code eruit halen. Remark de oude constructor en voeg deze toe:

  ```csharp
  public Settings(string xml)
  {
    var doc = XDocument.Parse(xml);

    DcomServer = doc?.Element("Settings")?.Element("dcomServer")?.Value;
    ConnectionString = doc?.Element("Settings")?.Element("connectionString")?.Value;
  }
  ```

- Is dit slim? Wat als we json willen ondersteunen? Zou het voor de gebruiker van een class duidelijk zijn wat te doen met een string constructor? Dit is een beter optie:

  ```csharp
  public static Settings LoadFromFile(string filePath)
  {
    var doc = XDocument.Load(filePath);

    return new Settings
    {
      DcomServer = doc?.Element("Settings")?.Element("dcomServer")?.Value,
      ConnectionString = doc?.Element("Settings")?.Element("connectionString")?.Value
    };
  }
  ```

## Demo 3b: Constructors volgorde

- Zet in `Runner.RunMe` de `StartSettings` om naar `StartOrdering`.

- Laat de code zien. Wat gebeurd er als DoNothingStatic wordt aangeroepen? Welke regels worden gelogd in welke volgorde?

- Remark de Shared constructor.

  - Laat de beforefieldinit gedrag zien, static worden niet geinitialiseerd.

- Open runner en remark `Ordering.DoNothingStatic()` en vervang dit door:

  ``` csharp
  var x = new Ordering();
  x.DoNothingInstance();
  ```

  - Wat gebeurd er als DoNothingInstance wordt aangeroepen? Welke regels worden gelogd in welke volgorde?

## Demo 3c: Destruction

- Zet in `Runner.RunMe` de `StartSettings` om naar `StartFinalize`. Laat de code zien.

- Laat de code zien. Wanneer wordt `RefHolder.~RefHolder` aangeroepen? Antwoord nooit!

- Zet onder aan `Runner.RunMe` de volgende code:

  ``` csharp
  GC.Collect(); //never do this in production code
  GC.WaitForPendingFinalizers(); //never do this in production code
  ```

- Run de code nog eens. De finalizers worden aangeroepen. Let op, ook volgorde is niet gedefinieerd. Wat doen we nu als we objecten, zoals file handles hebben die we zo snel mogelijk willen opschonen. Oplossing `IDisposable`

- Haal `GC.Collect();` en `GC.WaitForPendingFinalizers();` weg. Voeg de code aan `RefHolder` toe:

  ``` csharp
  class RefHolder : IDisposable
  {
    public void Dispose()
    {
      Console.WriteLine($"Dispose: {_number}");
    }
  ```

- Dispose moet expliciet woredn aangeroepen. Zet onder in de `StartFinalize` functie deze aanroepen:

  ``` csharp
  x.Dispose();
  y.Dispose();
  ```
 
- Wat gebeurd er als `x.Reference = y` een exception opgooid. Dan wordt de dispose niet aangeroepen. We moeten dit in een try .. catch gaan zetten, maar dat geeft veel code. Gelukkig heeft C# een helper keyword. Ze deze code in de `StartFinalize` functie

  ``` csharp
  using var x = new RefHolder(1);
  using var y = new RefHolder(2);
  x.Reference = y;
  y.Reference = x;
  ```

- Run de code nog eens. De Dispose worden aangeroepen. Dit gaat nu vanuit de normale thread en op gedefinieerde volgorde.

## Demo 3d: Singleton patroon

- Zet in `Runner.RunMe` de `StartSettings` om naar `StartSingleton`. Laat de code zien.

- Perfecte werkende implementatie, maar waar zit de fout?

  - Singleton deelt nu data over threads, maar er wordt daar geen rekening mee gehouden. SingletonCounter moet dus thread safe worden gemaakt. Pas de `StartSingleton` methode als volgt aan:

    ``` csharp
    private static void StartSingleton()
    {
      const int count = 1000;

      ThreadPool.QueueUserWorkItem(IncAndPrintSingleton);

      for(var i = 0; i < count; i++)
      {
        ThreadPool.QueueUserWorkItem(IncAndPrintSingleton);
      }

      while (ThreadPool.CompletedWorkItemCount < count)
      {
        Thread.Sleep(100);
      }

      IncAndPrintSingleton(null);
    }
    ```

- Run de code. Het probleem verschijnt.

### Singleton zonder data

- Singleton zonder data welke nooit data gaat bevatten; Gebruik `static class` (bv als je het abstract factory / factory method pattern gebruikt).

### Singleton instantie per thread

- Een andere optie is een Singleton instantie per thread. Omdat de data niet over theads gedeeld wordt hoeft er geen rekening gehouden te worden met thread safety. Pas de code aan:

  ``` csharp
  [ThreadStatic]
  private static readonly Singleton _instance = new Singleton();
  ```
  
- Run de code. Dit gaat fout! Waarom? Wanneer worden shared constructors uitgevoerd? We kunnen dit aanpassen naar:
  
  ``` csharp
  [ThreadStatic]
  private static Singleton? _instance;
  public static Singleton Current => _instance ??= new Singleton();
  ```

- Let op: Gebruik de [ThreadStatic] gaat niet mee in de async / await context. Is dat nodig gebruik dan `AsyncLocal<>`. Overigens is daar ook een `ThreadLocal<>` tegenhanger van beschikbaar.

### Thread safe Singleton

- Thread safe Singleton, bijvoorbeeld met `lock`. Pas de code als volgt aan:

- Verwijder de `<ThreadStatic>`. Pas de code als volgt aan:

  ``` csharp
  private static readonly Singleton _instance = new Singleton();
  private static readonly object _locker = new object();

  public static Singleton Current
  {
    get
    {
      lock (_locker)
      {
        return _instance;
      }
    }
  }
  ```

- Run de code. Dit gaat fout! Waarom? Wanneer is de Current() methode klaar? Stap eventueel door de code.

- In plaats daarvan moeten we alle external methodes thread safe maken. 

  ``` csharp
  private static readonly Singleton _instance = new Singleton();
  private readonly object _locker = new object();
  private int _counter;

  public static Singleton Current => _instance;

  private Singleton()
  {
    _counter = 2;
  }

  public int SingletonCounter
  {
    get
    {
      lock (_locker)
      {
        return _counter;
      }
    }

    set
    {
      lock (_locker)
      {
        _counter = value;
      }
    }
  }
  ```

- Run de code. Dit gaat nog steeds fout (soms paar keer proberen)! Waar zit het probleem? We locken het opvragen van de singleton. De lock eindigt als de singleton opgevraagd is, ongeacht hoe lang we deze gebruiken. We willen een lock gedurende het opvragen en optellen van de _counter variable. Dit doen we als volgt.

  ``` csharp
  public int SingletonCounter
  {
    get
    {
      lock (_locker)
      {
        return _counter;
      }
    }
  }

  public void IncCounter()
  {
    lock (_locker)
    {
      _counter++;
    }
  }
  ```

- Let op: Gebruik je het singleton patroon dan altijd documenteren op de `Current` property of het of de data per thread wordt bijgehouden of gedeeld wordt (per AssemblyLoadContext).
- Let op: Heb je meerdere static variable of een static contructor in een class dan gebeurd de initialisatie mogelijk eerder op dan bij het aanroepen van de `Current` property. In dat geval kun je beter de `Lazy<>` gebruiken.

  ``` csharp
  private static readonly Lazy<Singleton> _instance = new(() => new Singleton());

  public static Singleton Current => _instance.Value;
  ```

## Demo 4a: Events en delegates

- Laat de `Runner`, `Printer` en `ConsoleClient` zien. De ConsoleClient wil nu weten wanneer het printen klaar is en of het gelukt is.

- Zet in printer: `public delegate void PrintFinishedEventHandler(Exception? ex);`. Dit is de definitie van de terug te geven data, compleet met naampje zodat het lekker herkenbaar is.

- Zet in printer: `private static PrintFinishedEventHandler? _printFinished`. Dit is de lijst met observers, de luisteraars naar de event.

- De `_finishedListeners` is private. We moeten dus een manier geven om je te subscriben.

  ```csharp
  public static event PrintFinishedEventHandler PrintFinished
  {
    add => _printFinished += value;
    remove => _printFinished -= value;
  }
  ```

- Nu nog even in de event raisen in de DoPrint: `_printFinished?.Invoke(null);`

- Zet in ConsoleClient Print methode de regel: `Printer.PrintFinished += PrintEvent`, en ookm de functie:

  ```csharp
  private void Printer_PrintFinished(Exception? ex)
  {
    Console.WriteLine("ConsoleClient: PrintFinished event received");
  }
  ```

- Runnen.

- Gelukkig heeft C# een korter schrijfwijze. Haal `_printFinished` variable weg en de public custom event. Voeg het volgende toe: `public static event PrintFinishedEventHandler? PrintFinished;`

- Runnen.

- Voeg tevens een `public delegate void PrintStartedEventHandler();` en `public static event PrintStartedEventHandler? PrintStarted;` en raise ook deze event.

- Het zou ook makkelijk zijn als alle events dezelfde signature hadden zodat handlers hergebruikt kunnen worden. 

- Verwijder `PrintFinishedEventHandler` delegate en maak een nieuwe class aan :

  ```csharp
  class PrintFinishedEventArgs(Exception? exception = null) : EventArgs
  {
    public Exception? Exception { get; } = exception;
  }
  ```

- Verander in de event `PrintFinished` naar `public static event EventHandler<PrintFinishedEventArgs>? PrintFinished;`

- Verander in de event `PrintStarted` naar `public static event EventHandler? PrintStarted;`

- Verander in de invokes naar `PrintFinished?.Invoke(null, new PrintFinishedEventArgs());` en `PrintStarted?.Invoke(null, EventArgs.Empty);`

- Run.

- Subscribe aan de PrintStarted methode aan de Printer_PrintFinished event door de signature aan te passen naar `private void Printer_PrintFinished(object? sender, EventArgs e)`.

- Run.

- Let op! Een event is een helper om een lijst van observers, dat betekend:

  - Een observers instance zal niet worden vrijgegeven in geheugen zolang hij nog ergens naar luistert. Concreet, ConsoleClient blijft in memory tot hij stopt met luisteren of tot hij Printer uit de lucht gaat.

  - Een event heeft state en moet dus hetzelfde behandeld worden als de andere state m.b.t. thread safety. Pas dus op met Shared of Module level events.

## Demo4b: UI Thread

- Zet  `Runner.RunMe` de `ConsoleClient` onder commentaar, Decomentariseer de `WpfClient`. Open de `WpfClient` en de-comentariseer de code.

- Run, dit gaat fout. Oorzaak, de event wordt geraised op een andere thread.

- Vervang `OnPrintFinished` door:

  ```csharp
  private void OnPrintFinished(object? sender, PrintFinishedEventArgs e)
  {
    if (!Dispatcher.CheckAccess())
    {
      _ = Dispatcher.BeginInvoke(() => OnPrintFinished(sender, e));
      return;
    }

    TextBox1.Text = "WpfClient: OnPrintFinished called";
  }
  ```

- Dit speelt in alle windows UI frameworks (winforms, wpf, maui), maar hoe weet ik als gebruiker van de printer class dat dit event op een andere thread komt? Documenteer!!!

## Demo 5a: Generics

- Laat de `Runner` zien en dat de implementatie van de Load. Dit is een patroon wat we vaker  zien. Jammer alleen van de cast. Kunnen we deze niet weg werken?

- Verander nu de load implementatie naar een generic versie.

  ```csharp
  public static T Load<T>(string filePath) where T:ISettings
  {
    using var stream = File.OpenRead(filePath);
    var len = stream.ReadByte();
    var buffer = new byte[len];
    stream.Read(buffer, 0, len);
    var typeName = Encoding.UTF8.GetString(buffer);
    return JsonSerializer.Deserialize<T>(stream)!;
  }
  ```

- We zouden de methode ook beschikbaar kunnen maken op afgeleide types `WebSettings` en `SqlSettings`.

  ```csharp
  public static WebSettings Load(string filePath)
  {
    return SettingsUtils.Load<WebSettings>(filePath);
  }
  ```

- Dit geef veel fijnere intellisense in de `Runner`.

## Demo 5b: Base classes & interfaces

- Laat de `Runner` zien, daarnaast `SqlSettings` en `WebSettings`. Het lijkt ons logisch dat wel settings altijd kunnen schrijven. Waar zouden we deze methodes plaatsen?

- Maak van de interface een base class. Verplaats de Save methode van `SettingsUtils` naar de base. Pas de code als volgt aan:

  ```csharp
  public abstract class Settings
  {
    public void Save(string filePath)
    {
      using var stream = File.Create(filePath);
      var data = Encoding.UTF8.GetBytes(GetType().AssemblyQualifiedName!);
      stream.WriteByte((byte)data.Length);
      stream.Write(data, 0, data.Length);
      JsonSerializer.Serialize(stream, this, GetType());
    }
  }
  ```

- Het zou lekker zijn als de load en save code bij elkaar staan. Verplaats de Load methode ook van `SettingsUtils` naar de base class.

- Kijk nu in de intellisense. Je ziet nu twee Load methode. Dat is niet echt mooi. Maak de methode op de base `protected` i.p.v. `public`. Nu heb je wel nette intellisense.

## Demo 6: Indexer en Yeald

- Laat de `Runner` zien, daarnaast `Person` en `PersonCollection` en vertel dat je hier een collectie van wilt maken.

- Open PersonCollection. 

  ```csharp
  public IEnumerator<Person> GetEnumerator()
  {
    foreach(var item in _person)
    {
      yield return item;
    }
  }
  ```

- Draai de runner. Laat zien dat deze bij iedere stap van de foreach wordt geëvalueerd. Vertel dat dit een potentiele uitdaging is als de collectie wijzigt gedurende de foreach.

- Onder water zorgt een `yield` ervoor dat we een object alloceren welke de positie bij houd, de `For Each` vraagt nu ook dit object op, we kunnen deze dus doorlussen zodat we geen dubbele allocatie hebben.

  ```csharp
  public IEnumerator<Person> GetEnumerator() => _person.GetEnumerator();
  ```

- Zeg dat je een persoon direct wil kunnen opvagen op positie in de lijst. Haal het commentaar bij de regel weg.

  ```csharp
  Console.WriteLine($"First: {persons[1].FirstName}, Last {persons[1].LastName}");
  ```

- Implementeer de indexer hiervoor.

  ```csharp
  public Person this[int index] => _person[index];
  ```

- Demo de indexer.

- Implementeer nu ook Implements `IEnumerable(Of Person)` Verwijder eerst de oude `GetEnumerator` en voeg dan de volgende code toe:

    ```csharp
    public IEnumerator<Person> GetEnumerator() => _person.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _person.GetEnumerator();
    ```

- Demo de code.

## Demo 7: Extend

- Laat de `Runner` zien. De `GetFullName` willen we herbruikbaar maken. Dit gaan we doen door een extension method (let op de namespace!).

  ```csharp
  namespace Demos.ExtensionMethods
  {
    internal static class PersonExtensions
    {
      public static string GetFullName(this Person p)
      {
        return $"{p.FirstName} {p.LastName}";
      }
    }
  }
  ```

- Roep de code aan en laat zien dat deze werkt.

- Ga nu naar de `Runner` van Collecties. Probeer daar de GetFullName aan te roepen. Intellisense geeft hem niet weer. Roep hem nu toch aan en de namespace wordt toegevoegd. Nu wel intellisense.

- Pas de namespace aan van `PersonExtensions` aan naar `Collecties`. Nu is hij wel atijd overal zichtbaar zolang je de dll heb gereferentieerd.

## Demo 8a: Functional programming

- Laat de `Runner` zien, daarnaast `Person` en `QuickSort`.

- De generic quicksort gebruikt nu de IComparable. Deze moet dus op de person class geimplementeerd worden.

- Helaas kun je op deze manier maar 1 sortering kwijt. We willen soms op voornaam, maar ook soms op Achternaam kunnen sorteren. We gaan dit mogelijk maken d.m.v. functional programming constructies.

- Voeg de volgende constructor en backing fields toe.
  ```csharp
  private readonly Func<T, T, int> _sorter;

  public QuickSort(Func<T, T, int> sorter)
  {
    _sorter = sorter;
  }
  ```
- Pas de class declaratie aan naar `class QuickSort<T>`. Verwijder dus de where.

- Pas de aanroepen naar `x.CompareTo(y)` aan naar `_sorter(x, y)`.

- Pas in `Runner` de regel aan naar `var sorter = new QuickSort<Person>((a, b) => a.FirstName.CompareTo(b.FirstName));`.

- Run de nieuwe code

- Haal IComparable uit person weg.

- Voeg een extra sortering to op achternaam en druk het resultaat ook af.

## Demo 8b: Functional programming

- Zet  `Runner.RunMe` de `QuickSort` onder commentaar, Decomentariseer de `Console.WriteLine(CallRemoteDivideCalculation());`.

- Leg uit en run de code.

- Je wil ook een versie maken voor Multiply. Kopieer de code van `CallRemoteDivideCalculation` naar `CallRemoteMultiplyCalculation` en roep `Multiply` ipv `service.Divide(12, 3)` aan.

- Run de nieuwe code

- Je begrijpt dat dit veel code dupliceert. Je wil nu Functional programming constructies gebruiken om de methode mee te geven. 

- Pas `CallRemoteMultiplyCalculation` aan naar `static int CallRemoteCalculation(Func<IPrjService, int> execute)` aan. Roep nu de `execute(service)` i.p.v. `service.Divide(12, 3)`aan.

- Pas de aanroep aan naar `Console.WriteLine(CallRemoteCalculation(s => s.Divide(12,3)));`.

- Je begrijpt hoeveel code en onderhoud dit kan schelen.

## Demo 9: Linq

- We hebben en quicksort geimplementeerd en d.m.v. functional programming dynamisch gemaakt. Constructies zoals filteren en sorteren komen zo vaak voor dat Microsoft hier Linq voor heeft ontwikkeld. Hierbij een voorbeeld waar we gaan filteren en sorteren.

  ```csharp
  var newPersons = persons
    .Where(p => p.FirstName!.StartsWith("B"))
    .OrderByDescending(p => p.LastName);
  ```
- Draai de code

- Zet met F9 een een break point op de foreach, binnen de foreach op de WriteLine en een breakpoint op de `p.FirstName!.StartsWith("B")` binnen de `.Where()`. Draai de code en zie dat je eerst alle keren de `p.FirstName!.StartsWith("B")` breakpoint raakt voordat je de `Console.WriteLine` breakpoint raakt. Verwijder de breakpoints.

- Microsoft heeft ook een extra taalcontructie toe genaamd 'Query Expression Syntax'. Je kan daarmee het statment herschrijven als volgt.

  ```csharp
  var newPersons = from p in persons
                   where p.FirstName!.StartsWith("B")
                   orderby p.FirstName descending
                   select p;
  ```
- Draai de code

## Demo 10: XML

- Heb je de demo nog nooit uitgevoerd op je laptop dan moet je eerst de methode `BuildUsingXmlWriter` uitvoeren zodat de xml lokaal gecreerd word. Draai alle test in een release build zonder debugger (CTRL-F5)

- DocumentSpeedTest. Draai de code laat zien hoeveel tijd en geheugen XmlDocument nodig heeft om een 100MB document in te lezen. (ca 3,6s / 500mb)

- Unremark de XDocument code, laat zien dat dit iets sneller is en minder geheugen gebruikt. (ca 2,8s / 390mb), daarnaast 'voelt' de code beter oa door de Load methode die nu een static factory method is. 

- OptimizedReadSpeedTest. Laat zien hoe je door XmlReader en XDocument te combineren een krachige combinatie krijgt. Er wordt nu maar steeds 1 persoon in memory gelezen waardoor de tijd verminderd en het geheugen bijna 30x kleiner is. (ca 1,9s / 12mb)

- Het leuke is dat linq ervoor zorgt dat je nog gewoon kan filteren op het gedeelte in memory voeg na de `StreamElementsFromFile(XmlFileName)` het volgende toe:

  ```csharp
  .Where(e => e.Element("FirstName")!.Value.StartsWith("Bill"));
  ```

- Voer de code uit. Het geheugengebruik en tijd is nauwelijks aangepast en alles is nog steeds zeer leesbaar.

- BuildUsingXmlWriter. Laat zien hoe je een groot document maakt met XmlWriter. (ca 1,6s / 16mb)

- BuildUsingXElement. Laat zien hoe je een groot document maakt met XElement. Hoewel dit vrij goed is komen we toch op meer dan 2x zo traag en 25x meer geheugen. (ca 3,3s / 350mb)

- Vervang in BuildUsingXElement de `XElement` door: `XStreamingElement`. Het wordt nu streaming geschreven, een stuk beter. (ca 2s / 15mb)

- OPTIONEEL: WriteTransformTest. Laat zien hoe we alles combineren om een transform te maken.

- Het gebeurd regelmatig dat we data uit xml om willen zetten naar .net objecten of omgekeerd. Dit gebeurd zo vaak dat ms een standaard serialiseer meeleverd.

- XmlSerializerTest. Laat zien hoe we dmv de `Serialization.XmlSerializer` kunnen serialiseren van en naar .net objecten. Laat ook de `Persons` en `Person` classes zien en vertel hoe de attributen de XmlSerializer beinvloeden.

## Demo 11: Json

- Heb je de demo nog nooit uitgevoerd op je laptop dan moet je eerst de methode `BuildUsingUtf8JsonWriter` uitvoeren zodat de json lokaal gecreerd word. Draai alle test in een release build zonder debugger (CTRL-F5)

- DocumentSpeedTest. Draai de code laat zien hoeveel tijd en geheugen JsonDocument nodig heeft om een 100MB document in te lezen. (ca 0,7s / 235mb)

- OptimizedReadSpeedTest. Laat zien hoe je door JsonSerializer.DeserializeAsyncEnumerable te gebruiken je snel leest. Er wordt nu maar steeds 1 persoon in memory gelezen waardoor de tijd verminderd en het geheugen bijna 30x kleiner is. (ca 1,8s / 15mb).

- BuildUsingUtf8JsonWriter. Laat zien hoe je een groot document maakt met Utf8JsonWriter. (ca 0,7s / 12,4mb)

- BuildUsingJsonElement. We combineren nu de Iterator en de Utf8JsonWriter om een soortgelijke streaming oplossing te hebben als bij Xml icm XStreamingElement. (ca 1,0s / 15mb)

- Het gebeurd regelmatig dat we data uit json om willen zetten naar .net objecten of omgekeerd. Dit gebeurd zo vaak dat ms een standaard serialiseer meeleverd.

- JsonSerializerTest. Laat zien hoe we dmv de `JsonSerializer` kunnen serialiseren van en naar .net objecten. Laat ook de `Persons` en `Person` classes zien en vertel hoe de attributen de JsonSerializer beinvloeden.  (ca 1,3s / 154mb)

## Demo 12: SQL

- Zorg dat je een lokale sql instantie .\profitsqldev hebt en voer daarop uit.

  ```sql
  create database demo
  GO
  use demo
  GO
  create table Persons
  (
    Id Uniqueidentifier primary key DEFAULT newid(),
    FirstName varchar(250) null,
    LastName varchar(250) not null,
  )
  GO
  insert Persons Values(Newid(), 'Bart', 'Vries')
  insert Persons Values(Newid(), 'Bart%', 'Vries Sr.')
  insert Persons Values(Newid(), 'Ruud', 'Boerboom')
  insert Persons Values(Newid(), 'Herman', 'Huijgen')
  insert Persons Values(Newid(), 'Peter', 'Mols')
  GO
  create table Users
  (
    Id Uniqueidentifier primary key DEFAULT newid(),
    [Name] varchar(250) null,
    [Password] varchar(250) null,
  )
  insert Users Values(Newid(), 'Bart Vries', 'Dit is geheim')
  ```

### SQL basics

- Open SqlCommandSelect. Laat zien hoeveel code nodig is om met sql te werken.

- De code bevat een sql injection bug. Wat kunnen we bij de `searchValue` zetten om dit te reproduceren? Antwoord: 
  - `searchValue = @"xxxx' union all select NEWID() as id, Name as firstname, 'junk' as lastname from sys.tables--'";` information disclosure
  - `searchValue = @"xxxx' union all select id, Name as firstname, password as lastname from Users--'";` information disclosure, Elevation of privilege
  - `searchValue = @"Bart' drop table persons --'";` tampering, denial of service

  - Oplossing, deze oplossing levert ook nog eens snellere sql op omdat sql de hash van het statement cached:

    ```csharp
    using var com = new SqlCommand($"select id, firstname, lastname from persons where firstname = @firstname", con);
    com.Parameters.AddWithValue("@firstname", searchValue);
    ```

- bij het vullen van het de `Person` instantie worden namen gekopieerd en staat er een hardcoded datatype. Dit is niet erg onderhoudbaar.

- Open SqlTableSelect. We hebben nu geen hardcoded datatype meer, maar veldnamen zijn nog steeds strings en de row geeft alles als object terug.

- Open Person, wat zou het mooi zijn als we hier strongly typed tegen kunnen programmeren.

### Dapper

- Open DapperSelect. Veel mooiere code. Deze simpele Object/relational mapper controleert de datatypes en ondersteund parameter mapping, maar geeft je nog steeds complete controle over de sql statement.
  - Opvallend punt. In dapper zien we `new {` staan. Dit is een constructie om een anonymous type aan te maken. Er wordt onder water een instantie aangemaakt welke geen naam heeft, maar wel een property `FirstName` heeft. Dapper maakt dankbaar gebruik van deze mogelijkheid in C#.

- Open SqlUpdate. Dit is een basis update. Ik wil bij 'Bart%' de typefout er uit halen en deze corriseren naar 'Bart'. Voer de code uit. Waarom krijgen we 2 rows affected? Antwoord: SQL injection bug. Hoe lossen we dit op? Twee opties:
  1. In .NET, vervang code: `com.Parameters.AddWithValue("@firstname", SqlEscape.LikeEscape("Bart%"));`
  2. In SQL, vervang code: `using var com = new SqlCommand("update persons set firstname = left(firstname, len(firstname) - 1) where firstname like '%' + REPLACE(REPLACE(REPLACE(@firstname, '[','[[]'),'%','[%]'),'_','[_')", con);`

- Open DapperUpdate. Dapper heeft dit hetzelfde probleem, ook dapper weet niet dat de parameter in een like gebruikt wordt. Er zijn wel Object/relational mappers welke wel begrijpen. Een voorbeeld daarvan in EntityFramework Core.

### EntityFramework Core

- Open EFCoreSelect. Om EFCore te gebruiken moeten we een context bouwen, Hier registreren we de POCO objecten welke we later kunnen gebruiken.

  ```csharp
  class MyDbContext: DbContext
  {
    private readonly string _connectionString;

    public DbSet<Person> Persons { get; set; }

    public MyDbContext(string connectionString)
    {
      _connectionString = connectionString;
    }

    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(_connectionString);
    }
  }
  ```

- Vervolgens kunnen we als volgt queryen.

  ```csharp
  public void EFCoreSelect()
  {
    using var context = new MyDbContext(_connectionString);
    foreach (var person in context.Persons.Where(p => p.FirstName!.StartsWith("Bart")))
    {
      Console.WriteLine("{0} {1}", person.FirstName, person.LastName);
    }
  }
  ```

- run de code. Laat eventueel een sql trace zien. Dit OR mapper framework schrijft dus de sql code voor jou.
  - Voordeel: Als alles dynamisch is kun je switchen van database leverancier.
  - Nadeel: Geen complete controle over de afgevuurde sql. Hier kun je op ingrijpen, maar daarmee doe je het bovenstaande voordeel teniet.

- Open EFCoreUpdate. Vervolgens kunnen we als volgt updaten:

  ```csharp
  public void EFCoreUpdate()
  {
    using var context = new MyDbContext(_connectionString);

    foreach (var person in context.Persons.Where(p => p.FirstName!.StartsWith("Bart%")))
    {
      person.FirstName = "Bart";
    }
    var rowsAffected = context.SaveChanges();
    Console.WriteLine($"Rows changed: {rowsAffected}");
  }
  ```
- Dit is niet de meest efficiente sql statement, maar het kan ook als volgt:

  ```csharp
  public void EFCoreUpdate()
  {
    using var context = new MyDbContext(_connectionString);

    var rowsAffected = context.Persons.Where(p => p.FirstName!.StartsWith("Bart%")).ExecuteUpdate(setters => setters.SetProperty(p => p.FirstName, "Bart"));
    Console.WriteLine($"Rows changed: {rowsAffected}");
  }
  ```

### Transactions

- Open SqlUpdateTransaction. Nu doen we dit in een transactie. Als we een error throwen voor de .Commit dan zal de transactie worden teruggedraaid. Voer de code uit en laat zien dat er in sql niets veranderd is.

- Open DapperUpdateTransaction. Dit is in de basis hetzelfde.

- Open DapperUpdateTransaction. Dit is in de basis hetzelfde.

- Open EFCoreUpdateTransaction. Dit is net een andere variant, omdat we geen connectie hebben.

  ```csharp
  public void EFCoreUpdateTransaction()
  {
    using var context = new MyDbContext(_connectionString);
    using var transaction = context.Database.BeginTransaction();

    var rowsAffected = context.Persons.Where(p => p.FirstName!.StartsWith("Bart%")).ExecuteUpdate(setters => setters.SetProperty(p => p.FirstName, "Bart"));
    Console.WriteLine($"Rows changed: {rowsAffected}");

    transaction.Commit();
  }
  ```
## Demo 13: Exceptions

- Laat de `Runner` zien. Run `demos.exe` vanaf de console. Laat zien dat de exception.ToString naar de console word geschreven. Standaard exception handling in .NET zal de applicatie afbreken bij een onverwachte fout en deze loggen (eventlog) of weergeven. Leg uit dat het logisch is dat je niet kan doorwerken met een overwachte fout omdat de staat van je applicatie onbekend is.
- Zet een try catch blok (lege catch) om de `ThrowException();` aanroep. Run de code nu. Let uit dat dit zeer gevaarlijke code is. Wat als er een out of memory exception optreed? Als je **echt** dit wilt doen documenteer de catch en zorg dat in de catch met een flag de ingeslikte exception wordt gelogd met een vorm van devtracing.

Voor een voorbeeld van devtracing zie de `MyEventSource` class. Zet `MyEventSource.Log.DevTrace` in de catch.

Wat als je op globaal niveau de exception wilt opvangen? Dit kan door de `AppDomain.CurrentDomain.UnhandledException` te gebruiken. Voeg de volgende code toe in de `Runner.RunMe`:

```csharp
    AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
    {
      var exception = e.ExceptionObject as Exception;
      if(exception != null)
      {
        File.WriteAllText("error.txt", "Unhandled exception: " + exception);
      }
    };
```

- Zet nu de volgende code in de `Runner.RunMe`:

```csharp
    var t = new Thread(ThrowException);
    t.Start();

    Console.ReadKey();
```

- Run de code. Wat gebeurd er? De applicatie crasht. Waarom? Ook bij een fout op een andere thread is de staat van de applicatie onbekend. De applicatie kan niet verder gaan. Dit is een bewuste keuze van Microsoft. Je kunt dit gedrag wel aanpassen, maar dat raden we af. Deze exception kan je ook opvangen door de `Thread.CurrentThread.UnhandledException` te gebruiken. Maar dan zit je op een andere thread, dus je kan in `Thread.CurrentThread.UnhandledException` eigenlijk geen ui tonen, omdat je potentieel op een andere thread zit. Daarnaast kan je ook een outofmemory hebben, dus de code in de UnhandledException handler moet klein en simpel zijn.
- Voeg de volgende code toe in de `Runner.RunMe`:
```csharp
    Thread.CurrentThread.UnhandledException += (sender, e) =>
    {
      var exception = e.ExceptionObject as Exception;
      if(exception != null)
      {
        File.WriteAllText("error.txt", "Unhandled thread exception: " + exception);
      }
    };
```
- Pas de code aan naar:

```csharp
    Task.Run(() =>
    {
      ThrowException();
    });

    Console.ReadKey();\
```

- Run de code. Wat gebeurd er? De applicatie crasht niet. Waarom? Vreemd genoeg heeft Microsoft dit gedrag aangepast voor de Task. Dit is erg vreemd en we raden dus ook aan om bij unhandeld task exceptions toch de applicatie te laten crashen. Dit kan door `TaskScheduler.UnobservedTaskException` op te vangen en een actie te ondernemen. Bijvoorbeeld door daar een `Environment.Exit` aan te roepen.

- Gebruik je 3de of ui partij frameworks dan is het goed om te weten dat deze vaak ook speciale exception handling hebben. Zo heeft wpf `App.DispatcherUnhandledException` om alle exception in ui acties af te kunenn vangen. Of `RxApp.DefaultExceptionHandler` om alle exceptions vanuit Rx af te vangen.

- We willen nu een custom exception maken met een extra property. Maak een nieuwe class aan `MyCustomException` en laat deze afleiden van `Exception`. Voeg een property `ErrorCode` toe. Pas de constructor aan zodat deze de ErrorCode kan instellen.

- Throw de exception en laat de app crashen. Je ziet nu dat in de informatie op de console de ErrorCode property niet wordt weergegeven. Dit komt omdat de `Exception.ToString()` methode deze property niet doorgeeft. Tip is bij het bouwen van custom exception properties altijd ToString() te overriden. Helaas heeft microsoft geen standaard helper hiervoor, maar je kunt het zelf maken. Toon de `MyException` class

## Demo 14: Parallel

### Geen await zorgt voor parallel

- Open WpfClient. Run de code. De code werkt. Maar de textbox geeft printing aan terwijl het printen nog niet afgelopen is. Blijkbaar draait er parellele code. Zowel het printen is bezig als de ui wordt biijgewerkt.

- Pas de code aan naar:

```csharp
private async void StartButton_Click(object sender, RoutedEventArgs e)
{
  _printer.Reset();
  TextBox1.Text = _printer.PrintStatus.ToString();
  await _printer.PrintAsync("Hallo Wereld");
  TextBox1.Text = _printer.PrintStatus.ToString();
}
```

- Run de code. Blijkbaar wordt de textbox nu bijgewerkt als het printen klaar is. Conclusie: async zorgt voor parallele code, await zorgt dat deze wel sequentieel wordt uitgevoerd. Een async functie op een object aanroepen zonder await zorgt voor parrallele code. We hebben gezegd dat dit alleen mag op objecten dit thread safe zijn. Je bent dus verplicht om te wachten met de 2de functie call op een object tot de async method klaar is tenzij anders gedocumenteerd door de bouwe van de `Printer` class.

### ConfigureAwait(false)

- Pas de code aan naar:

```csharp
private async void StartButton_Click(object sender, RoutedEventArgs e)
{
  _printer.Reset();
  TextBox1.Text = _printer.PrintStatus.ToString();
  await _printer.PrintAsync("Hallo Wereld").ConfigureAwait(false);
  TextBox1.Text = _printer.PrintStatus.ToString();
}
```

- Run de code. Eerst wat opvalt, code gaat nog steeds **na** elkaar, maar dit geeft nu een exception omdat het op een threadpool thread wordt uitgevoerd. Standaard await gedrag is dat de code na de await wordt uitgevoerd op de thread die wordt bepaald door de `SynchronizationContext`. ConfigureAwait(false) zorgt ervoor dat de SynchronizationContext vervalt naar een standaard threadpool context. Dit is tevens de default. 
Echter in UI applicaties (WPF, WinForms, IOS, Android, enz) is deze synchronization context op de ui thread ook de ui thread. In de UI applicaties heeft `ConfigureAwait(false)`, dus meerwaarde, in de overige applicaties is het een noop, het doet feitelijk niets.

```csharp
private async void StartButton_Click(object sender, RoutedEventArgs e)
{
  await RunMe();
  TextBox1.Text = _printer.PrintStatus.ToString();
}

private async Task RunMe()
{
  _printer.Reset();
  TextBox1.Text = _printer.PrintStatus.ToString();
  await _printer.PrintAsync("Hallo Wereld").ConfigureAwait(false);
}
```

- We zien hier dat de code in de await in de `RunMe` methode wordt uitgevoerd op een threadpool thread. Maar na de await in de `StartButton_Click` methode wordt de code weer uitgevoerd op de ui thread. Omdat dit alleen speelt in UI apps op de UI thread hebben we bij AFAS besloten nergens ConfigureAwait(false) aan te roepen, ook niet in libraries, muv in ui code waar het ook daadwerkelijk tot ander gedrag leidt.

### Thread safe en await

- Pas de code aan naar:

```csharp
private async void StartButton_Click(object sender, RoutedEventArgs e)
{
  _printer.Reset();
  TextBox1.Text = _printer.PrintStatus.ToString();
  var t1 = _printer.PrintAsync("Hallo Wereld1");
  var t2 = _printer.PrintAsync("Hallo Wereld2");
  TextBox1.Text = _printer.PrintStatus.ToString();
}
```

- Run de code. Wat is de waarde in de textbox? Door geen await aan te roepen hebben we parallele code gecreeerd. Eerder hebben we gezegd dat instance classes niet thread safe hoeven te zijn. Dat geldt ook i.c.m. async. Tenzij anders gedocumenteerd door de programmeur mag je dus niet `PrintAsync` aanroepen tenzij de eerste `PrintAsync` klaar is. Je moet dus een nieuw `Printer` object creeren wil je 2 prints parallel uitvoeren.

