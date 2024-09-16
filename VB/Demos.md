# Effectief VB.Net programmeren - Demos

## Demo 1: Onderscheid tussen Structures en classes

- Laat code zien en vraag wat er geschreven gaat worden.

- Pas de Move methode aan naar `ByRef` en vraag wat het antwoord gaat worden.

- Haal de `ByRef` weg en pas de `Structure` aan naar `Class` en vraag wat het antwoord gaat worden.

- Pas de Move methode aan en voeg onder aan de methode `point = New Point() With {.X = 6}` toen en vraag wat het antwoord gaat worden.

- Pas de Move methode aan naar `ByRef` en vraag wat het antwoord gaat worden.

- Haal nog de `ByRef` weg. Pas de Move methode nog 1x aan naar struct en vraag wat het antwoord gaat worden.

- Haal de `ByRef` weg  en vraag wat het antwoord gaat worden.

- Zet voor de Move methode `p = nothing` en vraag wat het antwoord gaat worden.

- Vervang de `Move` methode door de onderstaande code:

  ```vbnet
  Public Sub Move(point As Point)
    If point Is Nothing Then
      point = New Point() With {.X = 6}
    End If
  End Sub
  ```

- Dit compileert niet. Vraag waarom niet.

- Maak van `Public Sub Move(point As Point?)` en laat zien dat de code dan wel compileert.

- Pas de `Dim p As Point` regel aan naar `Dim p As Point?`. Fix de Console.WriteLine.

- Run de code. Je krijgt {"Nullable object must have a value."}. 

## Demo 2a: Fields en thread safety

- Ze de code op release build en draai de code met CRTL-F5 om te zorgen dat er geen debugger aan hangt.

- We bouwen een simpele methode die een teller bij houd hoe vaak deze gebruiker de methode heeft aangeroepen.

  - Toon ModuleVar. Laat zien dat de `Me.` hier niet beschikbaar is.

  - Run methode, de code werkt

- We gaan nu deze methode aanroepen in een multi-thread omgeving, dit kan zijn de webserver, profitserver maar ook de afasman als er een backgroundworker draait.

- Pas de RunMe aan zodat de code onder commentaar wordt uitgevoerd.

- `Runner` heeft inconsistent gedrag. Dit komt omdat een shared methode state gebruikt, maar niet thread safe geprogrammeerd is.

## Demo 2b: Fields en thread safety

-   Stel ik ben gebruiker (en niet bouwer) van de `PrintCalls` methode. Maar deze library wordt gebruikt door diverse partijen. Hoe weet ik nu dat een andere partij de `PrintCalls` methode op een andere thread aanroept? dat kan ik niet weten...

- Vervang de `PrintCalls` methode door de onderstaande code:

  ```vbnet
  Private Sub AddAndPrintValueWithModule()
    dim x as new ClassVar()
    x.PrintCalls()
    x.PrintCalls()
    x.PrintCalls()
  End Sub
  ```
- Run de code. Er is nu verwacht gedrag, maar geen teller over de gehele applicatie heen.

- Laat zien dat in `PrintCalls` op de class `Me.` wel beschikbaar is.

- Verplaats `dim x as new ClassVar()` naar module niveau. Het onverwacht gedrag komt nu weer terug. Echter nu heb ik er zelf voor gekozen de `x` te delen over threads.

## Demo 3a: Constructors vs Construction methods

- Laat de `Settings` class zien en vraag wat je hier anders zou doen.

- Het in lezen van een bestand is potentieel zwaar en heeft een redelijke kans dat het tot exceptions leidt. Beide goede redenen om dit niet in een constructor te plaatsen. Laten we die code eruit halen. Remark de oude constructor en voeg deze toe:

  ```vbnet
  Public Sub New(xml As String)
    Dim doc = XDocument.Parse(xml)

    DcomServer = doc.Element("Settings").Element("dcomServer").Value
    ConnectionString = doc.Element("Settings").Element("connectionString").Value
  End Sub
  ```

- Is dit slim? Wat als we json willen ondersteunen? Zou het voor de gebruiker van een class duidelijk zijn wat te doen met een string constructor? Dit is een beter optie:

  ```vbnet
  Public Shared Function LoadFromFile(filePath As String) as Settings
    Dim doc = XDocument.Load(filePath)

    Return New Settings With {
      .DcomServer = doc.Element("Settings").Element("dcomServer").Value,
      .ConnectionString = doc.Element("Settings").Element("connectionString").Value
    }
  End Function
  ```

## Demo 3b: Constructors volgorde

- Zet in `Runner.RunMe` de `StartSettings` om naar `StartOrdering`.

- Laat de code zien. Wat gebeurd er als DoNothingShared wordt aangeroepen? Welke regels worden gelogd in welke volgorde?

- Remark de Shared constructor.

  - Laat de beforefieldinit gedrag zien, Shared worden niet geinitialiseerd.

- Open runner en remark `Ordering.DoNothingShared()` en vervang dit door:

  ``` vbnet
  Dim x = new Ordering()
  x.DoNothingInstance()
  ```

  - Wat gebeurd er als DoNothingInstance wordt aangeroepen? Welke regels worden gelogd in welke volgorde?

## Demo 3c: Destruction

- Zet in `Runner.RunMe` de `StartSettings` om naar `StartFinalize`. Laat de code zien.

- Laat de code zien. Wanneer wordt `RefHolder.Finalize` aangeroepen? Antwoord nooit!

- Zet onder aan `Runner.RunMe` de volgende code:

  ``` vbnet
  GC.Collect 'never do this in production code
  GC.WaitForPendingFinalizers 'never do this in production code
  ```

- Run de code nog eens. De finalizers worden aangeroepen. Let op, ook volgorde is niet gedefinieerd. Wat doen we nu als we objecten, zoals file handles hebben die we zo snel mogelijk willen opschonen. Oplossing `IDisposable`

- Haal `GC.Collect` en `GC.WaitForPendingFinalizers` weg. Voeg de code aan `RefHolder` toe:

  ``` vbnet
  Implements IDisposable

  Public Sub Dispose() Implements IDisposable.Dispose
    Console.WriteLine("Disposing " & _number)
  End Sub
  ```

- Dispose moet expliciet worden aangeroepen. Zet onder in de `StartFinalize` functie deze aanroepen:

  ``` vbnet
  x.Dispose()
  y.Dispose()
  ```
 
- Wat gebeurd er als `x.Reference = y` een excpetion opgooid. Dan wordt de dispose niet aangeroepen. We moeten dit in een try .. catch gaan zetten, maar dat geeft veel code. Gelukkig heeft vb.net een helper keyword. Ze deze code in de `StartFinalize` functie

  ``` vbnet
  Using x As New RefHolder(1)
    Using y As New RefHolder(2)
      x.Reference = y
      y.Reference = x
    End Using
  End Using
  ```

- Run de code nog eens. De Dispose worden aangeroepen. Dit gaat nu vanuit de normale thread en op gedefinieerde volgorde.

## Demo 3d: Singleton patroon

- Zet in `Runner.RunMe` de `StartSettings` om naar `StartSingleton`. Laat de code zien.

- Perfecte werkende implementatie, maar waar zit de fout?

  - Singleton deelt nu data over threads, maar er wordt daar geen rekening mee gehouden. SingletonCounter moet dus thread safe worden gemaakt. Pas de `StartSingleton` methode als volgt aan:

    ``` vbnet
    Private Sub StartSingleton()
      Const count As Integer = 1000

      ThreadPool.QueueUserWorkItem(AddressOf IncAndPrintSingleton)

      For i As Integer = 1 To count
        ThreadPool.QueueUserWorkItem(AddressOf IncAndPrintSingleton)
      Next

      While ThreadPool.CompletedWorkItemCount < count
        Thread.Sleep(100)
      End While

      IncAndPrintSingleton(Nothing)
    End Sub
    ```

- Run de code. Het probleem verschijnt.

### Singleton zonder data

- Singleton zonder data welke nooit data gaat bevatten; Gebruik `Module` (bv als je het abstract factory / factory method pattern gebruikt).

### Singleton instantie per thread

- Een andere optie is een Singleton instantie per thread. Omdat de data niet over theads gedeeld wordt hoeft er geen rekening gehouden te worden met thread safety. Pas de code aan:

  ``` vbnet
  <ThreadStatic>
  Shared ReadOnly _instance As New Singleton()
  ```
  
- Run de code. Dit gaat fout! Waarom? Wanneer worden shared constructors uitgevoerd? We kunnen dit aanpassen naar:
  
  ``` vbnet
  Public Shared ReadOnly Property Current() As Singleton
  Get
    if _instance Is Nothing Then
      _instance = New Singleton
    End If
    Return _instance
  End Get
  End Property
  ```
### Thread safe Singleton

- Thread safe Singleton, bijvoorbeeld met `SyncLock`.

- Verwijder de `<ThreadStatic>`.

- Voeg een `Shared _locker As New Object` toe en pas de Current methode als volgt aan:

  ``` vbnet
  Public Shared ReadOnly Property Current() As Singleton
    Get
      SyncLock _locker
        Return _instance
      End SyncLock
    End Get
  End Property
    ```

- Run de code. Dit gaat fout! Waarom? Wanneer is de Current() methode klaar? Stap eventueel door de code.

- In plaats daarvan moeten we alle external methodes thread safe maken. 

  ``` vbnet
  Shared _instance As New Singleton
  Private _locker As New Object
  Private _counter As Integer 'Protected by _locker

  Public Property SingletonCounter() As Integer
    Get
      SyncLock _locker
        Return _counter
      End SyncLock
    End Get
    Set(value As Integer)
      SyncLock _locker
        _counter = value
      End SyncLock
    End Set
  End Property
  ```

- Run de code. Dit gaat nog steeds fout (soms paar keer proberen)! Waar zit het probleem? We locken het opvragen van de singleton. De lock eindigt als de singleton opgevraagd is, ongeacht hoe lang we deze gebruiken. We willen een lock gedurende het opvragen en optellen van de _counter variable. Dit doen we als volgt.

  ``` vbnet
  Public ReadOnly Property SingletonCounter() As Integer
    Get
      SyncLock _locker
        Return _counter
      End SyncLock
    End Get
  End Property

  Public Sub IncCounter()
    SyncLock _locker
      _counter += 1
    End SyncLock
  End Sub
  ```

- Let op gebruik je het singleton patroon dan altijd documenteren op de `Current` property of het of de data per thread wordt bijgehouden of gedeeld wordt (per AssemblyLoadContext).

## Demo 4a: Events en delegates

- Laat de `Runner`, `Printer` en `ConsoleClient` zien. De ConsoleClient wil nu weten wanneer het printen klaar is en of het gelukt is.

- Zet in printer: `Public Delegate Sub PrintFinishedEventHandler(ex As Exception)`. Dit is de definitie van de terug te geven data, compleet met naampje zodat het lekker herkenbaar is.

- Zet in printer: `Private _finishedListeners As New List(Of PrintFinishedEventHandler)`. Dit is de lijst met observers, de luisteraars naar de event.

- De `_finishedListeners` is private. We moeten dus een manier geven om je te subscriben.

  ```vbnet
  Public Custom Event PrintFinished As PrintFinishedEventHandler
    AddHandler(value As PrintFinishedEventHandler)
      _finishedListeners.Add(value)
    End AddHandler
    RemoveHandler(value As PrintFinishedEventHandler)
      _finishedListeners.Remove(value)
    End RemoveHandler
    RaiseEvent(ex As Exception)
      For Each handler As PrintFinishedEventHandler In _finishedListeners
        handler(ex)
      Next
    End RaiseEvent
  End Event
  ```

- Nu nog even in de event raisen in de DoPrint: `RaiseEvent PrintFinished(Nothing)`

- Zet in ConsoleClient Print methode de regel: `AddHandler Printer.PrintFinished, AddressOf OnPrintFinished`

- Voeg de methode toe:

  ```vbnet
  Private Sub OnPrintFinished(ex As Exception)
    Console.WriteLine("ConsoleClient: OnPrintFinished called")
  End Sub
  ```

- Runnen.

- Gelukkig heeft vb.net een korter schrijfwijze. Haal `_finishedListeners` variable weg en de public custom event. Voeg het volgende toe: `Public Event PrintFinished As PrintFinishedEventHandler`

- Runnen.

- Voeg tevens een `Public Delegate Sub PrintStartedEventHandler()` en `public event PrintStarted as PrintStartedEventHandler` en raise ook deze event.

- Het zou ook makkelijk zijn als alle events dezelfde signature hadden zodat handlers hergebruikt kunnen worden. 

- Verwijder `PrintFinishedEventHandler` delegate en maak een nieuwe class aan :

  ```vbnet
  Public class PrintFinishedEventArgs
    Inherits EventArgs
    Sub New (Optional ex As Exception = Nothing)
      Exception = ex
    End sub
    Readonly Property Exception As Exception 
  End Class
  ```

- Verander in de event `PrintFinished` naar `Public Event PrintFinished As EventHandler(Of PrintFinishedEventArgs)`

- Verander in de event `PrintStarted` naar `Public Event PrintStarted As EventHandler(Of EventArgs)`

- Verander in de `RaiseEvent` naar `RaiseEvent PrintFinished(Nothing, new PrintFinishedEventArgs)` en `RaiseEvent PrintStarted(Nothing, EventArgs.Empty)`

- Run.

- Subscribe aan de PrintStarted methode aan de Printer_PrintFinished event door de signature aan te passen naar `Private Sub OnPrintFinished(sender As Object, e As EventArgs)`.

- Run.

- Let op! Een event is een helper om een lijst van observers, dat betekend:

  - Een observers instance zal niet worden vrijgegeven in geheugen zolang hij nog ergens naar luistert. Concreet, ConsoleClient blijft in memory tot hij stopt met luisteren of tot hij Printer uit de lucht gaat.

  - Een event heeft state en moet dus hetzelfde behandeld worden als de andere state m.b.t. thread safety. Pas dus op met Shared of Module level events.

## Demo4b: UI Thread

- Zet  `Runner.RunMe` de `ConsoleClient` onder commentaar, Decomentariseer de `WpfClient`. Open de `WpfClient` en de-comentariseer de code.

- Run, dit gaat fout. Oorzaak, de event wordt geraised op een andere thread.

- Vervang `OnPrintFinished` door:

  ```vbnet
  Private Sub OnPrintFinished(sender As Object, e As PrintFinishedEventArgs)
    If not Dispatcher.CheckAccess then
      Dispatcher.BeginInvoke(New EventHandler(Of
      PrintFinishedEventArgs)(AddressOf OnPrintFinished), sender, e)

      return
    End If

    TextBox1.Text = "WpfClient: OnPrintFinished called"
  End Sub
  ```

- Dit speelt in alle windows UI frameworks (winforms, wpf, maui), maar hoe weet ik als gebruiker van de printer class dat dit event op een andere thread komt? Documenteer!!!

## Demo 5a: Generics

- Laat de `Runner` zien en dat de implementatie van de Load. Dit is een patroon wat we vaker  zien. Jammer alleen van de DirectCast. Kunnen we deze niet weg werken?

- Verander nu de load implementatie naar een generic versie.

  ```vbnet
  Function Load(Of T As ISettings)(filePath As String) As T

    Using stream = File.OpenRead(filePath)
      Dim len = stream.ReadByte()
      Dim buffer(len - 1) As Byte
      stream.Read(buffer, 0, len)

      Dim typeName = Encoding.UTF8.GetString(buffer)
      return JsonSerializer.Deserialize(of T)(stream)
    End Using
  End Function
  ```

- We zouden de methode ook beschikbaar kunnen maken op afgeleide types `WebSettings` en `SqlSettings`.

  ```vbnet
  Public Shared Function Load(fileName As String) As WebSettings
    Return Load(Of WebSettings)(fileName)
  End Function
  ```

- Dit geef veel fijnere intellisense in de `Runner`.

## Demo 5b: Base classes & interfaces

- Laat de `Runner` zien, daarnaast `SqlSettings` en `WebSettings`. Het lijkt ons logisch dat wel settings altijd kunnen schrijven. Waar zouden we deze methodes plaatsen?

- Maak van de interface een base class. Verplaats de Save methode van `SettingsUtils` naar de base. Pas de code als volgt aan:

  ```vbnet
  Public MustInherit Class Settings
    Public Sub Save(filePath As String)
      Using stream = File.Create(filePath)
        Dim data = Encoding.UTF8.GetBytes([GetType].AssemblyQualifiedName)
        stream.WriteByte(CType(data.Length, Byte))
        stream.Write(data, 0, data.Length)
        JsonSerializer.Serialize(stream, Me, [GetType])
      End Using
    End Sub
  End Class
  ```

- Het zou lekker zijn als de load en save code bij elkaar staan. Verplaats de Load methode ook van `SettingsUtils` naar de base class.

- Kijk nu in de intellisense. Je ziet nu twee Load methode. Dat is niet echt mooi. Maak de methode op de base `Protected`. Nu heb je wel nette intellisense.

## Demo 6: Indexer en Yeald

- Laat de `Runner` zien, daarnaast `Person` en `PersonCollection` en vertel dat je hier een collectie van wilt maken.

- Open PersonCollection. 

  ```vbnet
  Public Iterator Function GetEnumerator() As IEnumerator(Of Person) 
    For Each i in _persons
      Yield i
    Next
  End Function
  ```

- Draai de runner. Laat zien dat deze bij iedere stap van de foreach wordt geëvalueerd. Vertel dat dit een potentiele uitdaging is als de collectie wijzigt gedurende de foreach.

- Onder water zorgt een `Public Iterator` ervoor dat we een object alloceren welke de positie bij houd, de `For Each` vraagt nu ook dit object op, we kunnen deze dus doorlussen zodat we geen dubbele allocatie hebben.

  ```vbnet
  Public Function GetEnumerator() As IEnumerator(Of Person)
      Return _persons.GetEnumerator()
  End Function
  ```

- Zeg dat je een persoon direct wil kunnen opvagen op positie in de lijst. Haal het commentaar bij de regel weg.

  ```vbnet
  Console.WriteLine("First: {0}, Last {1}", persons(1).FirstName, persons(1).LastName)
  ```

- Implementeer de indexer hiervoor.

  ```vbnet
  Default Public ReadOnly Property Item(index As Integer) As Person
    Get
      Return _persons(index)
    End Get
  End Property
  ```

- Demo de indexer.

- Implementeer nu ook Implements `IEnumerable(Of Person)` Verwijder eerst de oude `GetEnumerator` en voeg dan de volgende code toe:

    ```vbnet
    Implements IEnumerable(Of Person)

    Public Function GetEnumerator() As IEnumerator(Of Person) Implements IEnumerable(Of Person).GetEnumerator
      return _persons.GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
      return _persons.GetEnumerator()
    End Function
    ```

- Demo de code. Verwijder de `Default Public ReadOnly Property Item(index As Integer) As Person` functie, waarom compileert de code nog steeds? VB.NET roept intern een hulper functie aan die de positie bepaald door door de GetEnumerator heen te lopen. Dit kunnen we zien door een breakpoint te zetten. De aanwezigheid van de indexer maakt deze code dus veel sneller!

## Demo 7: Extend

- Laat de `Runner` zien. De `GetFullName` willen we herbruikbaar maken. Dit gaan we doen door een extension method (let op de namespace!).

  ```vbnet
  Namespace ExtensionMethods
    Module PersonExtensions
      <Extension>
        Function GetFullName(p As Person) As String
          Return $"{p.FirstName} {p.LastName}"
        End Function
    End Module
  End Namespace
  ```

- Roep de code aan en laat zien dat deze werkt.

- Ga nu naar de `Runner` van Collecties. Probeer daar de GetFullName aan te roepen. Intellisense geeft hem niet weer. Roep hem nu toch aan en de namespace wordt toegevoegd. Nu wel intellisense.

- Pas de namespace aan van `PersonExtensions` aan naar `Collecties`. Nu is hij wel atijd overal zichtbaar zolang je de dll heb gereferentieerd.

## Demo 8a: Functional programming

- Laat de `Runner` zien, daarnaast `Person` en `QuickSort`.

- De generic quicksort gebruikt nu de IComparable. Deze moet dus op de person class geimplementeerd worden.

- Helaas kun je op deze manier maar 1 sortering kwijt. We willen soms op voornaam, maar ook soms op Achternaam kunnen sorteren. We gaan dit mogelijk maken d.m.v. functional programming constructies.

- Voeg de volgende constructor en backing fields toe.
  ```vbnet
  Private _sorter As Func(Of T, T, Integer)

  Public Sub New(sorter As Func(Of T, T, Integer))
    _sorter = sorter
  End Sub
  ```
- Pas de class declaratie aan naar `Public Class QuickSort(Of T)`.

- Pas de aanroepen naar `x.CompareTo(y)` aan naar `_sorter(x, y)`.

- Pas in `Runner` de regel aan naar `Dim sorter = New QuickSort(Of Person)(Function(a, b) a.FirstName.CompareTo(b.FirstName))`.

- Run de nieuwe code

- Haal IComparable uit person weg.

- Voeg een extra sortering to op achternaam en druk het resultaat ook af.

## Demo 8b: Functional programming

- Zet  `Runner.RunMe` de `QuickSort` onder commentaar, Decomentariseer de `Console.WriteLine(CallRemoteDivideCalculation());`.

- Leg uit en run de code.

- Je wil ook een versie maken voor Multiply. Kopieer de code van `CallRemoteDivideCalculation` naar `CallRemoteMultiplyCalculation` en roep `Multiply` ipv `service.Divide(12, 3)` aan.

- Run de nieuwe code

- Je begrijpt dat dit veel code dupliceert. Je wil nu Functional programming constructies gebruiken om de methode mee te geven. 

- Pas `CallRemoteMultiplyCalculation` aan naar `Shared Function CallRemoteCalculation(execute As Func(Of IPrjService, Integer)) As Integer` aan. Roep nu de `execute(service)` i.p.v. `service.Divide(12, 3)`aan.

- Pas de aanroep aan naar `Console.WriteLine(CallRemoteCalculation(Function(service) service.Divide(12,3)))`.

- Je begrijpt hoeveel code en onderhoud dit kan schelen.

## Demo 9: Linq

- We hebben en quicksort geimplementeerd en d.m.v. functional programming dynamisch gemaakt. Constructies zoals filteren en sorteren komen zo vaak voor dat Microsoft hier Linq voor heeft ontwikkeld. Hierbij een voorbeeld waar we gaan filteren en sorteren.

  ```vbnet
  Dim newPersons = persons _
      .Where(Function(p) p.FirstName.StartsWith("B")) _
      .OrderByDescending(Function(p) p.LastName) 
  ```
- Draai de code

- Zet met F9 een een break point op de For Each, binnen de For Each op de WriteLine en een breakpoint op de `Function(p)` binnen de `.Where()`. Draai de code en zie dat je eerst alle keren de `Function(p)` breakpoint raakt voordat je de `Console.WriteLine` breakpoint raakt. Verwijder de breakpoints.

- Microsoft heeft ook een extra taalcontructie toe genaamd 'Query Expression Syntax'. Je kan daarmee het statment herschrijven als volgt.

  ```vbnet
  Dim newPersons = From p in persons
                   Where p.FirstName.StartsWith("B")
                   Order By p.LastName Descending
  ```
- Draai de code

## Demo 10: XML

- Heb je de demo nog nooit uitgevoerd op je laptop dan moet je eerst de methode `BuildUsingXmlWriter` uitvoeren zodat de xml lokaal gecreerd word. Draai alle test in een release build zonder debugger (CTRL-F5)

- DocumentSpeedTest. Draai de code laat zien hoeveel tijd en geheugen XmlDocument nodig heeft om een 100MB document in te lezen. (ca 3,8s / 500mb)

- Unremark de XDocument code, laat zien dat dit iets sneller is en minder geheugen gebruikt. (ca 3,0s / 390mb), daarnaast 'voelt' de code beter oa door de Load methode die nu een static factory method is. 

- OptimizedReadSpeedTest. Laat zien hoe je door XmlReader en XDocument te combineren een krachige combinatie krijgt. Er wordt nu maar steeds 1 persoon in memory gelezen waardoor de tijd verminderd en het geheugen bijna 30x kleiner is. (ca 2s / 12mb)

- Het leuke is dat linq ervoor zorgt dat je nog gewoon kan filteren op het gedeelte in memory voeg na de `StreamElementsFromFile(XmlFileName)` het volgende toe:

  ```vbnet
  .Where(Function(e) e.Element("FirstName").Value.StartsWith("Bill"))
  ```

- Voer de code uit. Het geheugengebruik en tijd is nauwelijks aangepast en alles is nog steeds zeer leesbaar.

- BuildUsingXmlWriter. Laat zien hoe je een groot document maakt met XmlWriter. (ca 1,5s / 12mb)

- BuildUsingXElement. Laat zien hoe je een groot document maakt met XElement. Hoewel dit vrij goed is komen we toch op meer dan 2x zo traag en 25x meer geheugen. (ca 3,5s / 350mb)

- Vervang in BuildUsingXElement de `XElement` door: `XStreamingElement`. Het wordt nu streaming geschreven, een stuk beter. (ca 2s / 14mb)

- OPTIONEEL: WriteTransformTest. Laat zien hoe we alles combineren om een transform te maken.

- Het gebeurd regelmatig dat we data uit xml om willen zetten naar .net objecten of omgekeerd. Dit gebeurd zo vaak dat ms een standaard serialiseer meeleverd.

- XmlSerializerTest. Laat zien hoe we dmv de `Serialization.XmlSerializer` kunnen serialiseren van en naar .net objecten. Laat ook de `Persons` en `Person` classes zien en vertel hoe de attributen de XmlSerializer beinvloeden.

## Demo 11: Json

- Heb je de demo nog nooit uitgevoerd op je laptop dan moet je eerst de methode `BuildUsingUtf8JsonWriter` uitvoeren zodat de json lokaal gecreerd word. Draai alle test in een release build zonder debugger (CTRL-F5)

- DocumentSpeedTest. Draai de code laat zien hoeveel tijd en geheugen JsonDocument nodig heeft om een 100MB document in te lezen. (ca 0,8s / 235mb)

- OptimizedReadSpeedTest. Laat zien hoe je door JsonSerializer.DeserializeAsyncEnumerable te gebruiken je snel leest. Er wordt nu maar steeds 1 persoon in memory gelezen waardoor de tijd verminderd en het geheugen bijna 30x kleiner is. (ca 1,7s / 14mb). We gebruiken hier `ToBlockingEnumerable` omdat VB.Net wat beperkt is in het werken met async. Meer info in de `ReadFromFileAsync` functie.

- BuildUsingUtf8JsonWriter. Laat zien hoe je een groot document maakt met Utf8JsonWriter. (ca 0,7s / 12,4mb)

- BuildUsingJsonElement. We combineren nu de Iterator en de Utf8JsonWriter om een soortgelijke streaming oplossing te hebben als bij Xml icm XStreamingElement. (ca 1,1s / 17mb)

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
  - `searchValue = "xxxx' union all select NEWID() as id, Name as firstname, 'junk' as lastname from sys.tables--'"` information disclosure
  - `searchValue = "xxxx' union all select id, Name as firstname, password as lastname from Users--'"` information disclosure, Elevation of privilege
  - `searchValue = "Bart' drop table persons --'"` tampering, denial of service

  - Oplossing, deze oplossing levert ook nog eens snellere sql op omdat sql de hash van het statement cached:

    ```vbnet
    Using com = New SqlCommand($"select id, firstname, lastname from persons where firstname = @firstname", con)

      com.Parameters.AddWithValue("@firstname", searchValue)
    ```

- bij het vullen van het de `Person` instantie worden namen gekopieerd en staat er een hardcoded datatype. Dit is niet erg onderhoudbaar.

- Open SqlTableSelect. We hebben nu geen hardcoded datatype meer, maar veldnamen zijn nog steeds strings en de row geeft alles als object terug.

- Open Person, wat zou het mooi zijn als we hier strongly typed tegen kunnen programmeren.

### Dapper

- Open DapperSelect. Veel mooiere code. Deze simpele Object/relational mapper controleert de datatypes en ondersteund parameter mapping, maar geeft je nog steeds complete controle over de sql statement.
  - Opvallend punt. In dapper zien we `New With` staan. Dit is een constructie om een anonymous type aan te maken. Er wordt onder water een instantie aangemaakt welke geen naam heeft, maar wel een property `FirstName` heeft. Dapper maakt dankbaar gebruik van deze mogelijkheid in VB.NET.

- Open SqlUpdate. Dit is een basis update. Ik wil bij 'Bart%' de typefout er uit halen en deze corriseren naar 'Bart'. Voer de code uit. Waarom krijgen we 2 rows affected? Antwoord: SQL injection bug. Hoe lossen we dit op? Twee opties:
  1. In .NET, vervang code: `com.Parameters.AddWithValue("@firstname",  SqlEscape.LikeEscape("Bart%"))`
  2. In SQL, vervang code: `Using com = New SqlCommand("update persons set firstname = left(firstname, len(firstname) - 1) where firstname like '%' + REPLACE(REPLACE(REPLACE(@firstname, '[','[[]'),'%','[%]'),'_','[_')", con)`

- Open DapperUpdate. Dapper heeft dit hetzelfde probleem, ook dapper weet niet dat de parameter in een like gebruikt wordt. Er zijn wel Object/relational mappers welke wel begrijpen. Een voorbeeld daarvan in EntityFramework Core.

### EntityFramework Core

- Open EFCoreSelect. Om EFCore te gebruiken moeten we een context bouwen, Hier registreren we de POCO objecten welke we later kunnen gebruiken.

  ```vbnet
  Public Class MyDbContext
    Inherits DbContext

    Private ReadOnly _connectionString As String
    Public Property Persons As DbSet(Of Person)

    Public Sub New(connectionString As String)
      _connectionString = connectionString
    End Sub


    Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
      optionsBuilder.UseSqlServer(_connectionString)
    End Sub
  End Class
  ```

- Vervolgens kunnen we als volgt queryen.

  ```vbnet
  Public Sub EFCoreSelect()
    Using context = New MyDbContext(_connectionString)

      For Each person In context.Persons.Where(Function(p) p.FirstName.StartsWith("Bart"))
        Console.WriteLine("{0} {1}", person.FirstName, person.LastName)
      Next
    End Using
  End Sub
  ```

- run de code. Laat eventueel een sql trace zien. Dit OR mapper framework schrijft dus de sql code voor jou.
  - Voordeel: Als alles dynamisch is kun je switchen van database leverancier.
  - Nadeel: Geen complete controle over de afgevuurde sql. Hier kun je op ingrijpen, maar daarmee doe je het bovenstaande voordeel teniet.

- Open EFCoreUpdate. Vervolgens kunnen we als volgt updaten:

  ```vbnet
  Public Sub EFCoreUpdate()
    Using context = New MyDbContext(_connectionString)

      For Each person In context.Persons.Where(Function(p) p.FirstName.StartsWith("Bart%"))
        person.FirstName = "Bart"
      Next
      dim rowsAffected = context.SaveChanges()
      Console.WriteLine($"Rows changed: {rowsAffected}")
    End Using
  End Sub
  ```
- Dit is niet de meest efficiente sql statement, maar het kan ook als volgt:

  ```vbnet
  Public Sub EFCoreUpdate()
    Using context = New MyDbContext(_connectionString)
      Dim rowsAffected = context.Persons.Where(Function(p) p.FirstName.StartsWith("Bart%")).ExecuteUpdate(Function(setters) setters.SetProperty(Function(p) p.FirstName, "Bart"))
      Console.WriteLine($"Rows changed: {rowsAffected}")
    End Using
  End Sub
  ```

### Transactions

- Open SqlUpdateTransaction. Nu doen we dit in een transactie. Als we een error throwen voor de .Commit dan zal de transactie worden teruggedraaid. Voer de code uit en laat zien dat er in sql niets veranderd is.

- Open DapperUpdateTransaction. Dit is in de basis hetzelfde.

- Open DapperUpdateTransaction. Dit is in de basis hetzelfde.

- Open EFCoreUpdateTransaction. Dit is net een andere variant, omdat we geen connectie hebben.

  ```vbnet
  Public Sub EFCoreUpdateTransaction()
    Using context = New MyDbContext(_connectionString)
      Using tran = context.Database.BeginTransaction()

        Dim rowsAffected = context.Persons.Where(Function(p) p.FirstName.StartsWith("Bart%")).ExecuteUpdate(Function(setters) setters.SetProperty(Function(p) p.FirstName, "Bart"))
        Console.WriteLine($"Rows changed: {rowsAffected}")

        tran.Commit() 'if this line is not called, the changes are not committed
      End Using
    End Using
  End Sub
  ```