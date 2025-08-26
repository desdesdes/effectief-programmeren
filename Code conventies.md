# Code standaarden

# Algemeen

## Algemeen

De ontwikkelstandaarden bestaan uit regels en aanbevelingen met betrekking tot specifieke ontwikkelonderwerpen. Elke ontwikkelaar dient bekend te zijn met en zich te houden aan deze standaarden tijdens het ontwikkelen. De standaarden zijn gebaseerd op [Microsoft coding conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) en [Microsoft Framework design guidelines](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/).

Woordenboek
|Woord|Betekenis|
|---|---|
|Overloading|Het definiëren van meerdere methoden met dezelfde naam, maar met verschillende parameters.|
|Extern|Verwijst naar een methode of functie die buiten je eigen assembly te benaderen is, dit omvat dus alles met uitzondering van `private` en `internal`. `protected internal` is ook extern.|

#### G001: Regels moeten te allen tijde worden gevolgd

Uitzonderingen op regels zijn niet toegestaan. Er kan een verzoek worden ingediend om de regel te wijzigen, of de code moet worden aangepast zodat deze aan de regel voldoet.

Sommige regels worden gevalideerd tijdens statische code analyse. Wanneer de code niet aan deze regels voldoet, zal de codeanalyse fouten opleveren. Daarnaast worden veel technische regels die niet in de ontwikkelstandaarden zijn gedocumenteerd, gecontroleerd tijdens statische codeanalyse.

#### G002: Elke keer dat een aanbeveling niet wordt gevolgd, moet dit een goede reden hebben en gedocumenteerd worden

Uitzonderingen op aanbevelingen zijn alleen in uitzonderlijke gevallen toegestaan. Indien nodig kan een verzoek worden ingediend om een aanbeveling te wijzigen. Uitzonderingen op aanbevelingen moeten tijdens een code review worden besproken. Goede redenen omvatten geen persoonlijke stijlvoorkeuren. Wanneer een aanbeveling niet wordt gevolgd, moet dit in de code of in de onderdrukking van de codeanalysewaarschuwing worden gedocumenteerd.

Sommige aanbevelingen worden gevalideerd tijdens statische codeanalyse in Visual Studio. Wanneer de code niet aan deze aanbevelingen voldoet, zal de codeanalyse een waarschuwing geven. Daarnaast worden veel technische regels die niet in de ontwikkelstandaarden zijn gedocumenteerd, gecontroleerd tijdens statische codeanalyse. In uitzonderlijke gevallen kunnen deze waarschuwingen worden onderdrukt. Onderdrukkingen moeten zo dicht mogelijk bij het probleem worden gemaakt en een rechtvaardiging bevatten. Als je een globale onderdrukking nodig hebt, maak of gebruik dan een *GlobalSuppressions.cs*-bestand op het hoofdniveau van het project, maar gebruik dit alleen als er geen andere optie is.

#### G003: Voorzie regel- en aanbevelingsnummers van het voorvoegsel dat overeenkomt met het onderwerp en verander het nummer nooit

Nummers mogen nooit worden gewijzigd, omdat dit alle verwijzingen naar de regel of aanbeveling ongeldig zou maken. Er is een voorvoegsel voor elk onderwerp.

Standaarden met betrekking tot een specifiek ontwikkelonderwerp moeten onder het overeenkomstige onderwerp worden gedocumenteerd. Als een regel betrekking heeft op meerdere onderwerpen, moet de regel worden toegevoegd aan het onderwerp dat het meest specifiek is voor de regel. Bijvoorbeeld, wanneer een regel de naamgeving van Exceptions beschrijft, moet de regel worden toegevoegd aan de *Naamgevingsstandaarden*, omdat alle naamgevingsstandaarden zijn opgenomen in de *Naamgevingsstandaarden*. Echter, wanneer een regel betrekking heeft op het specifieke ontwerp van exceptions, moet deze worden toegevoegd aan de *Exception Regels*, omdat de *Ontwerpregels* alleen algemene ontwerpkwesties bevatten.

|**Onderwerp**|**Voorvoegsel**|**Beschrijving**|
| :--- | :--- | :--- |
|Algemeen|G|Standaarden die niet in andere categorieën vallen|
|Naamgeving|N|Alles met betrekking tot naamgeving|
|Ontwerp|D|Algemeen gebruik van objectoriëntatie en algemeen codeontwerp|
|Onderhoudbaarheid|M|Codeconstructies en syntaxis|
|Prestaties|P|Prestaties|
|UI|U|UI - UX|

# Naamgeving

## Algemeen

#### N001: Gebruik Amerikaans-Engels als naamgevingstaal

Gebruik waar mogelijk Amerikaans-Engels voor de naamgeving van identifiers.

Nederlands moet worden gebruikt voor uitsluitend Nederlandse zakelijke termen. Voor- en achtervoegsels van de identifier moeten in het Engels zijn.
Voorbeelden:
- _Inkooporder_ in het Nederlands is _Purchase order_ in het Engels. De identifier moet _PurchaseOrder_ heten, niet _InkoopOrder_, omdat de Engelse term ook zinvol is.
- _Kleine Ondernemers Regeling_ is een regeling voor kleine bedrijven in Nederland. De identifier moet _KleineOndernemersRegeling_ heten, omdat de vertaalde term _SmallBusinessRegulation_ in het Engels geen betekenis heeft, omdat de regeling daar niet bestaat.
- Een functie om de tabs voor _KleineOndernemersRegeling_ op te halen, moet _GetKleineOndernemersRegelingTabs_ heten en niet _VerkrijgKleineOndernemersRegelingTabbladen_. Dit komt omdat Engelse en Nederlandse termen gemengd kunnen worden en dit zou resulteren in een mix van voor- en achtervoegsels in de code, wat het erg moeilijk maakt om functies te vinden.

#### N002: Geef een identifier een naam op basis van zijn betekenis en niet zijn type

Vermijd het gebruik van taalspecifieke terminologie in de namen van identifiers. Stel bijvoorbeeld dat je een aantal overbelaste methoden hebt om gegevenstypen naar een stream te schrijven:

```csharp
void Write(double doubleValue); 	//Fout
void Write(long longValue); 		//Fout
void Write(double value); 		//ok
void Write(long value); 		//ok
```

```vbnet
Sub Write(doubleValue as Double); 	'Fout
Sub Write(longValue as Long); 		'Fout
Sub Write(value as Double); 		'ok
Sub Write(value as Long); 		'ok
```

Als het absoluut noodzakelijk is om voor elk gegevenstype een uniek benoemde methode te hebben, gebruik dan Universal Type Names in de methodenamen. De onderstaande tabel geeft de mapping van C#-typen naar universele typen.

|**C# Type Name**|**VB Type Name**|**Universal Type Name**|
| :--- | :--- | :--- |
|bool|Boolean|Boolean|
|byte|Byte|Byte|
|sbyte|SByte|SByte|
|char|Char|Char|
|decimal|Decimal|Decimal|
|double|Double|Double|
|float|Single|Single|
|int|Integer|Int32|
|uint|UInteger|UInt32|
|long|Long|Int64|
|ulong|ULong|UInt64|
|object|Object|Object|
|short|Short|Int16|
|ushort|UShort|UInt16|
|string|String|String|

Op basis van het bovenstaande voorbeeld kunnen de overeenkomstige leesmethoden er als volgt uitzien:

```csharp
double ReadDouble() { };
long ReadInt64() { };
```

```vbnet
Function ReadDouble() As Double;
Function ReadInt64() As Long;
```

**Opmerking:** de typenamen die in de Read-methoden worden gebruikt, zijn de universele typenamen!!!

#### N003: Gebruik GEEN Hongaarse notatie

Gebruik geen Hongaarse notatie of voeg geen andere type-identificatie toe aan identifiers. Het gebruik van Hongaarse notatie wordt afgeraden door bedrijven als Microsoft omdat het een programmeertaalafhankelijkheid introduceert en onderhoudsactiviteiten bemoeilijkt. Gebruik alleen de naamgevingstoevoegingen zoals beschreven in dit document.

#### N004: Gebruik GEEN underscores

Underscores zijn niet toegestaan in namen.

Uitzonderingen:
- Private fields *moeten* beginnen met een underscore-prefix
- Eventhandlers, bijv. okButton\_OnClick, ConfigProcessing\_Completed

#### N005: Gebruik GEEN standaard voor- of achtervoegsel voor identifiers

Voorzie klassen niet van het voor- of achtervoegsel *Class*, methoden met *Method*, structs met *Struct*, enums met *Enum*, delegates met *Delegate* etc.

#### N006: Geef het bronbestand de naam van de hoofdklasse

Plaats bovendien niet meer dan één top-level klasse in één bronbestand. Dit verbetert de vindbaarheid van classes.

Gebruik alleen `partial` classes als het andere deel van de class gegenereerd wordt. Gebruik geen partial om een class over meerdere uitgebrogrammeerde bestanden te verdelen. Dit maakt het terugzoeken van code erg lastig en debuggen met source links via symbols vrijwel onmogelijk.

#### N007: Gebruik vragende namen voor booleaanse velden, variabelen, properties en parameters

De naam van booleaanse velden, variabelen, properties en parameters moet altijd bestaan uit een werkwoord (Is, Has, Does, Can, etc.) en een bijvoeglijk naamwoord of zelfstandig naamwoord (Enabled, Visible, Dirty, UpdateNeeded, Children, etc.), bijvoorbeeld: IsEnabled, IsVisible, IsDirty, IsUpdateNeeded, HasChildren. Het moet mogelijk zijn om de naam te gebruiken als een vraag met twee mogelijke antwoorden: ja of nee. Als het (logisch) mogelijk is om de naam met 'Is' te beginnen, dan moet 'Is' worden gebruikt.

#### N008: Gebruik GEEN negaties

Gebruik nooit (impliciete) negaties bij het benoemen van variabelen, functies, properties, etc.

|**Fout**|**OK**|
| :--- | :--- |
|IsNotFilled|IsFilled|
|IsNotInitialized|IsInitialized|
|IsDisabled|IsEnabled|
|IsInvisible|IsVisible|
|IsHidden|IsDisplayed|

#### N009: Geef de voorkeur aan het niet gebruiken van het woord *object* in documentatie bij het verwijzen naar een type of instantie.

Het woord object is een zeer generiek woord en moet in documentatie worden vermeden. Het wordt vaak gebruikt om een type te beschrijven, maar even vaak om een instantie van het type te beschrijven. Geef de voorkeur aan het gebruik van het woord *instance* of *type*.

## Classes en Structs

#### N010: Gebruik een zelfstandig naamwoord voor de naamgeving van klassen

### Enums

#### N011: Gebruik een enum om parameters, properties en returntypes sterk te typeren

Dit verbetert de duidelijkheid en type-safety. Probeer casten tussen enumerated types en integrale types te vermijden.

#### N012: Gebruik enkelvoudige namen voor enumeratietypes die een keuze vertegenwoordigen

Noem bijvoorbeeld een enumeratietype niet Protocols maar noem het in plaats daarvan Protocol. Beschouw het volgende voorbeeld waarin slechts één optie is toegestaan.

#### N013: Gebruik een meervoudsvorm voor enumeraties die bitfields vertegenwoordigen

Het volgende codefragment is een goed voorbeeld van een enumeratie die het combineren van meerdere opties toestaat.

```csharp
[Flags]
public enum SearchOptions
{
    CaseInsensitive = 0b1,
    WholeWordOnly = 0b10,
    AllDocuments = 0b100,
    Backwards = 0b1000,
    AllowWildcards = 0b10000
}
```

```vbnet
<Flags>
Public Enum SearchOptions
    CaseInsensitive = &H01
    WholeWordOnly = &H02
    AllDocuments = &H04
    Backwards = &H08
    AllowWildcards = &H10
End Enum
```

#### N014: Voorzie (abstracte) (MustInherit in VB) basisklassen niet van het voor- of achtervoegsel *Base*

Er moet altijd een optie zijn om een basisklasse aan een klasse toe te voegen. Als de basisklasse *Base* heet, zou die naam BaseBase zijn? Ook maakt het woord base het niet duidelijker. Het is beter om een naam te gebruiken die beschrijft wat het type vertegenwoordigt.

SqlConnection is een goed voorbeeld van een klasse die een verbinding met een SQL-database vertegenwoordigt. Het heet niet SqlConnectionBase.

### Interfaces

#### N015: Interfaces moeten worden voorafgegaan door de letter I

Alle interfaces moeten worden voorafgegaan door de letter *I* ('I' hoofdletter). Gebruik een zelfstandig naamwoord (bijv. *IComponent*), een zelfstandig naamwoordgroep (bijv. *ICustomAttributeProvider*), of een bijvoeglijk naamwoord (bijv. *IPersistable*) om een interface te benoemen.

### Methoden

#### N016: Een methodenaam moet overeenkomen met het gedrag van de methode

Gebruik namen die echt overeenkomen met het functionele gedrag van een methode. Het mag niet worden vernoemd naar de interne implementatie of naar de aanroeper of aangeroepene. Als een methode niet het verwachte resultaat oplevert of het verwachte gedrag vertoont, moet deze een exception genereren.

Voorbeeld: Als een methode GetValue() heet, moet deze altijd de waarde retourneren. Maar als de waarde niet kan worden gevonden, opgehaald, etc. moet deze een exception gooien omdat het de waarde niet kan retourneren. 

Try: Als een methode kan mislukken vanwege bedrijfslogica (bijv. het wordt verwacht te mislukken), overweeg dan een equivalente methode toe te voegen die het mogelijk maakt om op succes te controleren zonder dat de exception wordt gegooid. Noem deze methode TryXxx(), die true retourneert voor succes/false voor mislukking indien mogelijk.

OrDefault: Soms wordt je code flow veel simpeler als je een functie hebt die de standaard waarde van een type teruggeeft i.p.v. een exceptions. Bv het lezen van een int uit een collectie waarbij je de waarde 0 mag gebruiken als deze niet in de collectie bestaat. Geef deze functie een naam waarmee het duidelijk is dat het de standaard waarde retourneert, bijv. GetValueOrDefault().

Voorbeeld:

```csharp
// Haal de waarde op die overeenkomt met de sleutel
// Gooi een exception als de sleutel niet bestaat
private string GetValue(string key)
{
}

// Haal de waarde op die overeenkomt met de sleutel
// Retourneert true als de sleutel is gevonden, anders false
private bool TryGetValue(string key, [NotNullWhen(true)]out string? value)
{
}

// Haal de waarde op die overeenkomt met de sleutel
// Retourneert null als de sleutel niet bestaat
private string? GetValueOrDefault(string key)
{
}

// Verwijder het opgegeven bestand
// Gooi een exception als het bestand niet kan worden verwijderd
private void DeleteFile(string filePath)
{
}

// Verwijder het opgegeven bestand
// Retourneert true als het bestand is verwijderd, anders false
private bool TryDeleteFile(string filePath)
{
}
```

```vbnet
' Haal de waarde op die overeenkomt met de sleutel
' Gooi een exception als de sleutel niet bestaat
Private Function GetValue(key as String) As String
End Function

' Haal de waarde op die overeenkomt met de sleutel
' Retourneert true als de sleutel is gevonden, anders false
Private Function TryGetValue(key As String, <Out, NotNullWhen(true)> ByRef value As String?) As Boolean
End Function

' Haal de waarde op die overeenkomt met de sleutel
' Retourneert null als de sleutel niet bestaat
Private Function GetValueOrDefault(key as String) As String?
End Function

' Verwijder het opgegeven bestand
' Gooi een exception als het bestand niet kan worden verwijderd
Private Sub DeleteFile(filePath As String)
End Sub

' Verwijder het opgegeven bestand
' Retourneert true als het bestand is verwijderd, anders false
Private Function TryDeleteFile(filePath As String) As Boolean
End Function
```

#### N017: Gebruik een werkwoord gevolgd door een zelfstandig naamwoord voor het benoemen van methoden

Gebruik eerst een werkwoord gevolgd door een zelfstandig naamwoord, bijvoorbeeld: GetEmployeeName, UpdateEmployee, IsValidUserName. Merk op dat properties niet aan deze regel hoeven te voldoen.

#### N018: Voeg GEEN 'Callback' of een vergelijkbaar achtervoegsel toe aan callback-methoden

Voeg geen achtervoegsels zoals *Callback* of *CB* toe om aan te geven dat methoden via een callback-delegate worden aangeroepen. Je kunt geen aannames doen over of methoden via een delegate worden aangeroepen of niet. Een eindgebruiker kan besluiten om Asynchronous Delegate Invocation te gebruiken om de methode uit te voeren.

```csharp
static void DemoCallback(System.IAsyncResult ar) { } // Fout
static void DemoCB(System.IAsyncResult ar) { }       // Fout
```

```vbnet
Sub DemoCallback(System.IAsyncResult ar) ' Fout
End Sub
Sub void DemoCB(System.IAsyncResult ar) ' Fout
End Sub
```

### Collecties

#### N019: Gebruik een meervoudsvorm voor variabelen die een array- of collectietype vertegenwoordigen

Gebruik meervoudsvormen voor arrays en collectietypes om duidelijk te maken dat het een array of collectie is.

## Hoofdlettergebruik

#### N020: Gebruik het juiste hoofdlettergebruik voor alle identifiers

|**Identifier**|**Case**|**Voorbeeld**|**Opmerking**|
| :--- | :--- | :--- | :--- |
|Namespace|Pascal|System.Drawing||
|Class|Pascal|AppDomain||
|Struct|Pascal|Record||
|Delegate|Pascal|Calculation||
|Interface|Pascal|IDisposable|Gebruik I, gevolgd door een hoofdletter|
|Enum value|Pascal|FatalError||
|Event|Pascal|ValueChanged||
|Event handler delegate|Pascal|ValueChanged**EventHandler**|Gebruik eventnaam + 'EventHandler', maar het gebruik van EventHandler<T> heeft de voorkeur|
|Event handler|Pascal|Item\_ValueChanged|Gebruik een veld- of beschrijvende naam + \_ + eventnaam|
|Field/Constant (private)|Camel|\_name|Uitzondering: UI-designer types hoeven niet met een underscore te beginnen.|
|Field/Constant (niet-private)|Pascal|Name||
|Method|Pascal|ToString||
|Property|Pascal|Name||
|Parameter/argument|Camel|typeName||
|Variable|Camel|color||
|Generic type|Pascal|TKey|Begin met T|

Tip:
Gebruik een .editorconfig-bestand om de regels voor hoofdlettergebruik af te dwingen. Dit bestand kan aan de root van de solution worden toegevoegd en wordt door Visual Studio gebruikt om de regels af te dwingen.

Opmerkingen:
Microsoft heeft meerdere concurrerende standaarden voor `static` velden, de ene gebruikt _ (hetzelfde als instance fields), de andere gebruikt s_ en weer een andere gebruikt s_ en t_. Wij hebben gekozen voor _.
- https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names
- https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-type-members#names-of-fields

#### N021: Gebruik het juiste hoofdlettergebruik voor afkortingen

Tweeletterige afkortingen in Pascal-casing hebben beide letters als hoofdletter. In Camel-casing geldt dit ook, behalve aan het begin van een identifier waar beide letters in kleine letters worden geschreven. Met betrekking tot hoofdlettergebruik in Pascal- en Camel-casing worden afkortingen met meer dan twee letters als gewone woorden behandeld. Enkele voorbeelden:

|**Camel Casing**|**Pascal Casing**|
| :--- | :--- |
|uiEntry|UIEntry|
|cmsEntry|CmsEntry|
|demoUI|DemoUI|

# Ontwerp
## Algemeen
### Constructors en destructors

#### D001: Maak geen constructor die geen volledig geinitialiseerde instantie oplevert

Maak alleen constructors die instanties construeren die volledig zijn geinitialiseerd. Het zou niet nodig moeten zijn om extra properties in te stellen.

#### D002: Roep niet direct of indirect `virtual` of `abstract` methoden (`Overridable` / `MustOverride` in VB) aan in een constructor

Dit veroorzaakt vreemd gedrag van de constructor wanneer de virtuele methode wordt aangeroepen, omdat de virtuele methode op het afgeleide type wordt uitgevoerd voordat de constructors volledig zijn uitgevoerd.

#### D003: Constructors horen snel te zijn.

Gebruikers van een class mogen verwachten dat constructors snel zijn. Als een constructor traag is, overweeg dan om een `static` methode (`Shared` in VB) te gebruiken die de instantie retourneert en maak de constructor `private`.

#### D004: Beperk het aantal exceptions dat vanuit een constructor wordt gegooid

Constructors horen slechts een beperkt aantal exceptions gooien. Als een constructor veel soorten exceptions kan gooien, overweeg dan om een `static` methode (`Shared` in VB) te gebruiken die de instantie retourneert en maak de constructor `private`.

Gooi nooit exceptions vanuit `static` constructors (`Shared` in VB), omdat ontwikkelaars dat gedrag niet verwachten en geen controle hebben over wanneer de `static` constructor wordt aangeroepen (`Shared` in VB).

### Velden

#### D005: Declareer velden nooit als extern

Velden mogen nooit als extern worden gedeclareerd.

Uitzonderingen:
- `static` readonly velden en constanten, die elke geschikte toegankelijkheid mogen hebben.

Niet-private velden gebruiken geen underscore-prefix, maar worden benoemd als een property (Pascal-cased).

#### D006: `static` methoden moeten veilig zijn voor parallel gebruik (`shared` / `module` in VB), instance methoden *kunnen* veilig zijn. {#D006}

Documenteer het als een klasse-instantie of bepaalde methodes parallel gebruikt mogen gebruikt is die niet gedocumenteerd dan is dit niet toegestaan.

Een `static` functie die niet parallel gebruikt kan worden gebruikt, mag niet bestaan.

Dit betekent in feite dat je geen `static` velden (`shared` velden of `module` velden in VB) moet gebruiken in een methode, tenzij het veld readonly en onveranderlijk is.

Parallel betekent dat meerdere aanroepen gelijktijdig bezig kunnen zijn door meerdere threads te gebruiken of door niet te wachten tot een async functie klaar is, dus async aanroep zonder await.

#### D007: Bouw geen write-only properties

Implementeer altijd een property getter of gebruik in plaats daarvan een methode.

### Methoden

#### D008: Maak functies `static` (`shared` in VB) als ze stateless en niet-exposed zijn

Een niet-exposed methode die geen instance fields gebruikt, moet `static` zijn (`shared` in VB). Een exposed methode die geen instance fields gebruikt, moet `static` zijn (`shared` in VB) als dit gepast is. Dit betekent dat het `static` moet worden gemaakt als je niet verwacht dat het in de toekomst instance fields zal gebruiken.

#### D009: Als je een public methode en een protected methode hebt met dezelfde naam, voeg dan het achtervoegsel *Core* of *Internal* toe aan de protected methode

Dit maakt het voor ontwikkelaars gemakkelijk te herkennen. De public methode moet de protected methode aanroepen en beide methoden moeten hetzelfde doel hebben en vergelijkbaar gedrag vertonen, anders moeten verschillende namen worden gebruikt.

## .NET

### Constructors en destructors

#### D010: Destructors moeten snel zijn (Finalizer in VB)

Destructors (Finalizer in VB) moeten snel zijn, omdat er maar één thread per proces is die de finalizer aanroept. Als de destructor traag is, kan het lang duren voordat andere destructors worden aangeroepen, wat het vermogen van .NET om resources vrij te geven beperkt.

Overweeg het IDisposable-patroon te gebruiken zodat je de destructor kunt vermijden d.m.v. `GC.SuppressFinalize()`.

### Types

#### D011: Deel geen type-instanties tussen threads, tenzij gedocumenteerd is dat ze thread-safe zijn.

Je moet er niet van uitgaan dat een instantie van een type thread-safe is, tenzij gedocumenteerd is dat dit het geval is. Zie ook [D006](#D006)

#### D012: Plaats externe types op het namespace-niveau in plaats van binnen een ander type

Plaats de externe types op het namespace-niveau, niet op klasseniveau.

Uitzonderingen: private klassen, structs, enums en delegates moeten genest zijn binnen een ander type.

### Klassen en structs

#### D013: Wanneer een struct te gebruiken in plaats van een klasse

Een *struct* mag worden overwogen voor types die aan de volgende criteria voldoen:
- Waardetype-semantiek is gewenst.
- Gedragen zich als primitieve types.
- Hebben een instantiegrootte van minder dan +/- 16 bytes.
- Zijn onveranderlijk (immutable).

Onthoud dat een *struct* niet kan worden afgeleid en geen door de gebruiker gedefinieerde parameterloze constructor kan bevatten.

#### D014: Extensiemethoden zijn alleen toegestaan als ze passend en zinvol zijn binnen hun namespace

Aangezien extensiemethoden zichtbaar zijn in de hele namespace waarin ze zijn gedefinieerd, moeten ze overal in die namespace dezelfde betekenis en relevant gebruik hebben.

Dus als een extensiemethode alleen relevant is voor een specifieke klasse, moet deze in dezelfde namespace als die klasse worden gedefinieerd. Als het relevant is voor een specifieke set klassen, moet het in dezelfde namespace als die klassen worden gedefinieerd. Als het relevant is voor een specifieke set klassen in een specifieke context, moet het in dezelfde namespace als die klassen in die context worden gedefinieerd.

Dit maakt het gemakkelijk om een specifieke extensiemethode te vinden met intellisense, maar voorkomt ook dat intellisense wordt vervuild met irrelevante extensiemethoden.

Stel je hebt een POCO-type Employee dat leeft in Afas.MyApp.Hrm.Employee. We hebben een extensiemethode ToJson die het type omzet naar een JSON-string.

We gebruiken het Employee-type in Afas.MyApp.Crm.Person. Als we de ToJson-methode alleen in de Person-context nodig hebben, moeten we deze in Afas.MyApp.Crm.Person plaatsen. Als we het alleen in de Crm-context nodig hebben, moeten we het in Afas.MyApp.Crm plaatsen. Maar als we het overal nodig hebben waar het Employee-type wordt gebruikt, moeten we het in Afas.MyApp.Hrm.Employee plaatsen. We zouden zelfs kunnen overwegen om het toe te voegen aan de System.Text.Json-namespace, zodat het alleen zichtbaar is als we die namespace toevoegen.

### Events en event handlers

#### D015: Gebruik een werkwoord voor het benoemen van een event

Goede voorbeelden van events zijn Closing, Minimized en Click.

Bijvoorbeeld, de declaratie voor het Closing-event kan er als volgt uitzien:

```csharp
public event EventHandler Closing;
```

```vbnet
Public Event Closing As EventHandler
```

#### D016: Voeg geen Event-achtervoegsel toe aan de naam van een event

Voeg geen *Event*-achtervoegsel of een ander type-gerelateerd achtervoegsel toe aan de naam van een event.

#### D017: Gebruik een -ing en -ed vorm om pre-events en post-events uit te drukken

Gebruik een *-ing* en *-ed* vorm om pre-events en post-events uit te drukken (*Closing* / *Closed*). Gebruik geen patroon zoals *BeginXxx* en *EndXxx*. Als je afzonderlijke events wilt aanbieden om een tijdstip voor en een tijdstip na een bepaalde gebeurtenis uit te drukken, zoals een validatie-event, gebruik dan geen patroon zoals *BeforeValidation* en *AfterValidation*. Gebruik in plaats daarvan een *Validating* en *Validated* patroon.

Typisch hebben post-events geen optie om de actie te annuleren, omdat deze al is uitgevoerd. Pre-events hebben deze optie meestal wel.

#### D018: Voorzie event-raising methoden van het voorvoegsel 'On'

Als een klasse kan worden overgeërfd (d.w.z. niet sealed), gebruik dan een virtuele methode om het event te raisen. Op die manier kan de afgeleide klasse het gedrag overschrijven en zelfs voorkomen dat het event wordt geraised. Deze methode moet OnEventName heten, waarbij EventName altijd de naam is van het event dat wordt geraised.

```csharp
public event EventHandler Completed;

public virtual void OnCompleted(EventArgs e)
{
    if (Completed != null)
    {
        Completed(this, e);
    }
}
```

```vbnet
Public Event Completed As EventHandler

Public Overridable Sub OnCompleted(e as EventArgs)
    RaiseEvent Completed(Me, e)
End Sub
```

#### D019: Gebruik Naam_EventNaam voor event-afhandelingsmethode

Event-afhandelingsmethoden kunnen ofwel een event van een specifiek veld afhandelen, of een gegeneraliseerde handler zijn. Bij het afhandelen van een event van een specifiek veld, gebruik de naam van dat veld (zonder '_' prefix) als de Naam in Naam\_EventNaam. Als de methode een gegeneraliseerde handler is, moet de Naam in Naam_EventNaam de bron van het event beschrijven.

```csharp
// Event handler voor een event van een specifieke variabele
private void okButton_Click(EventArgs e)
{
}

// Gegeneraliseerde event handler
private void Calculation_Completed(EventArgs e)
{
}
```

```vbnet
' Event handler voor een event van een specifieke variabele
Private Sub okButton_Click(e As EventArgs)
End Sub

' Gegeneraliseerde event handler
Private Sub Calculation_Completed(e As EventArgs)
End Sub
```

#### D020: Raise events via een `protected virtual` methode (`Protected Overridable` in VB) als de klasse niet `static` of `sealed` is (`Module` of `NotInheritable` in VB)

Als een afgeleide klasse het gooien van een event wil onderscheppen, kan het zo'n virtuele methode overschrijven, zijn eigen werk doen en dan beslissen of het de basisklasseversie al dan niet aanroept. Aangezien de afgeleide klasse kan besluiten de basisklassemethode niet aan te roepen, zorg ervoor dat het geen werk doet dat nodig is voor de basisklasse om correct te functioneren.

Noem deze methode *OnEventName*, waarbij EventName moet worden vervangen door de naam van het event. Merk op dat een event handler hetzelfde naamgevingsschema gebruikt maar een andere signatuur heeft. Het volgende fragment (de meeste delen weggelaten voor de beknoptheid) illustreert het verschil tussen de twee.

```csharp
// Een voorbeeldklasse
public class Connection
{
  // Event definitie
  public event EventHandler Closed;

  // Methode die het event veroorzaakt
  public void Close()
  {
    // Doe iets en raise dan het event
    OnClosed(EventArgs.Empty);
  }

  // Methode die het Closed event raiset.
  protected virtual void OnClosed(EventArgs args)
  {
    if (Closed != null)
    {
        Closed(this, args);
    }
  }

  // Hoofdingang
  public static void Main()
  {
    Connection connection = new Connection();
    connection.Closed += Connection_Closed;
  }

  // Event handler voor het Closed event.
  private static void Connection_Closed(object sender, EventArgs args)
  {
  }
}
```

```vbnet
public class Connection
    'Event definitie
    Public Event Closed As EventHandler

    ' Methode die het event veroorzaakt
    Public Sub Close()
        ' Doe iets en raise dan het event
        RaiseEvent Closed(Me, EventArgs.Empty)
    End Sub

    ' Methode die het Closed event raiset.
    Public Sub OnClosed(e As EventArgs)
        RaiseEvent Closed(Me, e)
    End Sub

    ' Hoofdingang
    Public Shared Sub Main()
        Dim connection As New Connection()
        AddHandler connection.Closed, AddressOf Connection_Closed
    End Sub

    Private Shared Sub Connection_Closed(sender As Object, e As EventArgs)
    End Sub
End Class
```

#### D021: Gebruik de sender/arguments-signatuur voor event handler delegates

Alle event handler delegates moeten de volgende signatuur hebben:

```csharp
void Xxx(object sender, EventArgs arguments);
```

```vbnet
Sub Xxx(sender As Object, arguments As EventArgs)
```

Het gebruik van de basisklasse als het sender-type stelt afgeleide klassen in staat om dezelfde event handler te hergebruiken. Hetzelfde geldt voor de arguments-parameter. Het wordt aanbevolen om af te leiden van de *EventArgs*-klasse van het .NET en je eigen event-data toe te voegen. Het gebruik van zo'n klasse voorkomt het vervuilen van de signatuur van de event handler, maakt het mogelijk om de event-data uit te breiden zonder bestaande gebruikers te breken, en kan meerdere waarden bevatten. Bovendien moeten alle event-data via properties worden blootgesteld, omdat dat verificatie mogelijk maakt en toegang tot data voorkomt die niet altijd geldig is in alle gevallen van een bepaald event. Overweeg het gebruik van de EventHandler<> generieke delegate voor de event-signatuur. Voorbeeld:

```csharp
public class MyEventArgs : EventArgs //Afleiden van EventArgs
{
    public MyEventArgs(string extraInfo)
    {
        ExtraInfo = extraInfo;
    }

    public string ExtraInfo { get; }
}
```

```vbnet
Public Class MyEventArgs
  Inherits EventArgs 'Afleiden van EventArgs

  Public Sub New(extraInfo as String)
    _extraInfo = extraInfo
  End Sub

  Public ReadOnly Property ExtraInfo As String
End Class
```

### Delegate types

#### D022: Voeg Callback toe aan delegate types die gerelateerd zijn aan callback-methoden

Delegate types die worden gebruikt om een verwijzing naar een callback-methode door te geven (dus geen event) moeten worden voorzien van het achtervoegsel Callback. Bijvoorbeeld:

```csharp
public delegate void AsyncIOFinishedCallback(IpcClient client, string message);
```

```vbnet
Public Delegate Sub AsyncIOFinishedCallback(client As IpcClient, message As String)
```

Voeg geen *Callback* toe aan callback-methoden!

#### D023: Gebruik waar mogelijk Action, Func en EventHandler generics

Het gebruik van generieke delegates zorgt voor efficiënter geheugengebruik en voorkomt dat de types worden vervuild met veel delegate types.

### Enums

#### D024: Gebruik een enum om parameters, properties en returntypes sterk te typeren

Dit verbetert de duidelijkheid en type-safety. Probeer casten tussen enumerated types en integrale types te vermijden.

#### D025: Gebruik het `[Flags]` (`<Flags>` in VB) attribuut op een enum als er een bitwise operatie op de numerieke waarden moet worden uitgevoerd

Gebruik een *enum* met het *flags* attribuut alleen als de waarde volledig kan worden uitgedrukt als een set bitvlaggen. Gebruik een meervoudsvorm voor zo'n *enum*. Gebruik geen *enum* voor open sets (zoals de besturingssysteemversie).

Gebruik geen sentinel-waarden om bijvoorbeeld de eerste of laatste waarde aan te duiden. Alle leden van de enum moeten een betekenis hebben die van toepassing is op de enum.

```csharp
// Het protocol is altijd een van deze
public enum Protocol
{
  Tcp,
  Udp,
  Http,
  Ftp,
  // Gebruik dit niet:
  ProtocolCount
}
```

```vbnet
' Het protocol is altijd een van deze
Public Enum Protocol
  Tcp
  Udp
  Http
  Ftp
  ' Gebruik dit niet:
  ProtocolCount
End Enum
```

#### D026: Gebruik delegate-inferentie in plaats van expliciete delegate-instantiatie

```csharp
delegate void SomeDelegate();

public void SomeMethod()
{
}

public void SomeMethod2()
{
  SomeDelegate del = SomeMethod; // OK
  SomeDelegate del = new SomeDelegate(SomeMethod); // Fout
}
```

```vbnet
Delegate Sub SomeDelegate()

Public Sub SomeMethod()
End Sub

Public Sub SomeMethod2()
  Dim del as SomeDelegate = AddressOf SomeMethod ' OK
  Dim del as new SomeDelegate(AddressOf SomeMethod) ' Fout
End Sub
```

#### D027: Geef de voorkeur aan lambda-expressies boven anonieme delegates

VB ondersteunt geen anonieme delegates.

```csharp
private Action _action;

public void Demo()
{
    _action = () => { Console.WriteLine("aa"); }; // OK
    _action = delegate { Console.WriteLine("aa"); }; // Fout
}
```

```vbnet
Private _action As Action

Public Sub Demo()
    _action = Sub() Console.WriteLine("aa") ' OK
End Sub
```

### Interfaces

#### D028: Overweeg attributen boven lege interfaces

Lege interfaces worden niet aanbevolen. Een interface is ontworpen als een contract, maar een leeg contract heeft geen betekenis. Overweeg in plaats daarvan een attribuut te gebruiken.

Een lege interface kan worden gebruikt om een set basisklassen of interfaces te groeperen, maar je moet overwegen om overloads voor de specifieke types te bouwen in plaats van de functie de interface te laten accepteren. Dit zorgt voor schonere code en betere prestaties.

#### D029: Gebruik expliciete interface-implementatie alleen om naamconflicten te voorkomen of om optionele interfaces te ondersteunen

Wanneer je expliciete interface-implementatie gebruikt, zijn de methoden die door de betreffende klasse worden geïmplementeerd niet zichtbaar via de klasse-interface. Om toegang te krijgen tot die methoden, moet je eerst de instantie naar de gevraagde interface casten. Het wordt niet aanbevolen om expliciete interface-implementatie te gebruiken.

Uitzonderingen:
- Wanneer je naamconflicten wilt voorkomen. Dit kan gebeuren wanneer meerdere interfaces moeten worden ondersteund die gelijk genaamde methoden hebben, of wanneer een bestaande klasse een nieuwe interface moet ondersteunen waarin de interface een lid heeft waarvan de naam conflicteert met een lid van de klasse.
- Wanneer je verschillende optionele interfaces wilt ondersteunen (bijv. *IEnumerator, IComparer*, etc.) en je je klasse-interface niet wilt vervuilen met hun leden.

Beschouw het volgende voorbeeld:

```csharp
public interface IFoo1
{
  void Foo();
}

public interface IFoo2
{
  void Foo();
}

public class FooClass : IFoo1, IFoo2
{
  // Deze Foo is alleen toegankelijk door expliciet te casten naar IFoo1
  void IFoo1.Foo() {  }

  // Deze Foo is alleen toegankelijk door expliciet te casten naar IFoo2
  void IFoo2.Foo() {  }
}
```

```vbnet
Public Interface IFoo
  Sub Foo()
End Interface

Public Interface IFoo2
  Sub Foo()
End Interface

Public Class FooClass
  Implements IFoo, IFoo2

  ' Deze Foo is alleen toegankelijk door expliciet te casten naar IFoo1
  Private Sub IFoo_Foo() Implements IFoo.Foo
  End Sub

  ' Deze Foo is alleen toegankelijk door expliciet te casten naar IFoo2
  Private Sub IFoo2_Foo() Implements IFoo2.Foo
  End Sub
End Class
```

#### D030: Voorzie attribuutklassen van het achtervoegsel Attribute

Klassen die een attribuut vertegenwoordigen, moeten altijd het achtervoegsel Attribute hebben. Bij het gebruik van een attribuut, laat het achtervoegsel weg:

```csharp
public class FormattingAttribute : Attribute
{
}

[FormattingAttribute("###-##-####")] // Fout, achtervoegsel wordt niet weggelaten
public int socialSecurityNumber;

[Formatting("###-##-####")]          // OK, achtervoegsel wordt weggelaten
public int socialSecurityNumber;
```

```vbnet
Public Class FormattingAttribute
  Inherits Attribute
End Class

<FormattingAttribute("###-##-####")> ' Fout, achtervoegsel wordt niet weggelaten
Public socialSecurityNumber As Integer

<Formatting("###-##-####")>          ' OK, achtervoegsel wordt weggelaten
Public socialSecurityNumber As Integer
```

#### D031: Voorzie exception-klassen van het achtervoegsel Exception

Voorzie exception-klassen van het achtervoegsel *Exception*.

Bijvoorbeeld: FileNotFoundException.

#### D032: Voorzie collecties van het juiste achtervoegsel

Voorzie de naam van types die IEnumberable implementeren van het achtervoegsel van het gedrag dat ze implementeren:

- Sleutel/Waarde-achtig gedrag: Dictionary
- Stack-achtig gedrag: Stack
- Queue-achtig gedrag: Queue
- Set-achtig gedrag (kan items slechts één keer bevatten): Set
- Lijst-achtig gedrag (indexer property): List
- Alle andere: Collection

#### D033: Voorzie `static` helperklassen van het achtervoegsel "Utility" of "Extensions"

We onderscheiden twee soorten helperklassen:

- `static` klassen die algemene leden bevatten voor een specifiek type of probleemdomein, deze klassen moeten "\<naam>Utility" heten.
- `static` klassen die extensiemethoden bevatten voor een specifiek type of probleemdomein, deze klassen moeten "\<naam>Extensions" heten.

Een 'Utility'-klasse mag geen extensiemethoden bevatten, en een 'Extensions'-klasse mag geen andere publieke niet-extensie helpermethoden bevatten.

### Constructors en destructors

#### D034: Benader geen referentietype-leden in de destructor

Wanneer de destructor wordt aangeroepen door de GC, is het zeer waarschijnlijk dat sommige of alle instanties waarnaar door klasseleden wordt verwezen, al zijn opgeruimd door de garbage collector, dus het dereferencen van die instanties kan exceptions veroorzaken. Alleen waardetype-leden kunnen worden benaderd (omdat ze op de stack leven).

#### D035: Implementeer bij het implementeren van een Destructor altijd het IDisposable-patroon

Als een destructor vereist is, houd je dan aan: [D036](#d036)

#### D036: Implementeer IDisposable als een klasse onbeheerde of dure resources gebruikt of andere IDisposables bevat (#D036)

Als een type onbeheerde resources gebruikt, zoals handles die worden geretourneerd door C/C++ code of IDisposable-resources, moet je de *IDisposable*-interface implementeren om typegebruikers in staat te stellen dergelijke resources expliciet vrij te geven. Implementeer altijd de destructor die Dispose(false) aanroept, zodat het mogelijk is om te detecteren wanneer Dispose() niet is aangeroepen. Aangezien Dispose() (die Dispose(true) aanroept) altijd moet worden aangeroepen op een IDisposable-instantie, mag Dispose(false) nooit voorkomen. Omdat de destructor wordt onderdrukt in de Dispose(), wordt deze alleen aangeroepen als de instantie niet expliciet is gedisposed. Om ervoor te zorgen dat resources altijd op de juiste manier worden opgeruimd, moet een Dispose-methode meerdere keren kunnen worden aangeroepen zonder een exception te gooien.

Het volgende codefragment toont het patroon dat voor dergelijke scenario's moet worden gebruikt:

```csharp
public class ResourceHolder : IDisposable
{
  // Implementatie van de IDisposable-interface
  public void Dispose()
  {
    // Roep interne Dispose(bool) aan
    Dispose(true);
    // Voorkom dat de destructor wordt aangeroepen
    GC.SuppressFinalize(this);
  }

  // Centrale methode voor het opruimen van resources
  protected virtual void Dispose(bool disposing)
  {
    // Als disposing waar is (wat altijd het geval zou moeten zijn),
    // dan is deze methode aangeroepen via de publieke Dispose()
    if(disposing)
    {
      // Geef beheerde resources vrij of ruim ze op, zoals IDisposable-instanties
    }

    // Geef altijd (eventuele) onbeheerde resources vrij of ruim ze op
  }

  // Voeg altijd de destructor toe (niet in afgeleide klassen, optioneel in een sealed klasse)
  ~ResourceHolder()
  {
    Dispose(false);
  }
}
```

```vbnet
Public Class ResourceHolder
  Implements IDisposable ' Implementatie van de IDisposable-interface

  Public Sub Dispose() Implements IDisposable.Dispose
    ' Roep interne Dispose(bool) aan
    Dispose(True)
    ' Voorkom dat de destructor wordt aangeroepen
    GC.SuppressFinalize(Me)
  End Sub

  ' Centrale methode voor het opruimen van resources
  Protected Overridable Sub Dispose(disposing As Boolean)
    ' Als disposing waar is (wat altijd het geval zou moeten zijn),
    ' dan is deze methode aangeroepen via de publieke Dispose()
    If disposing Then
      ' Geef beheerde resources vrij of ruim ze op, zoals IDisposable-instanties
    End If

    'Geef altijd (eventuele) onbeheerde resources vrij of ruim ze op
  End Sub

  ' Voeg altijd de destructor toe (niet in afgeleide klassen, optioneel in een NotInheritable klasse)
  Protected Overrides Sub Finalize()
    ' Wijzig deze code niet. Plaats opruimcode in de 'Dispose(disposing As Boolean)'-methode
    Dispose(False)
  End Sub
End Class
```

Als een andere klasse van deze klasse afleidt, dan moet deze klasse alleen de *Dispose(bool)*-methode van de basisklasse overschrijven. Het moet *IDisposable* niet zelf implementeren, noch een destructor voorzien. De destructor van de basisklasse wordt automatisch aangeroepen.

```csharp
public class DerivedResourceHolder : ResourceHolder
{
  protected override void Dispose(bool disposing)
  {
    if(disposing)
    {
      // Geef beheerde resources vrij of ruim ze op, zoals IDisposable-instanties
    }

    // Geef altijd (eventuele) onbeheerde resources vrij of ruim ze op
    // Roep Dispose aan op onze basisklasse.
    base.Dispose(disposing);
  }
}
```

```vbnet
Public Class DerivedResourceHolder
  Inherits ResourceHolder

  Protected Overrides Sub Dispose(disposing As Boolean)
    If disposing Then
      ' Geef beheerde resources vrij of ruim ze op, zoals IDisposable-instanties
    End If

    ' Geef altijd (eventuele) onbeheerde resources vrij of ruim ze op
    ' Roep Dispose aan op onze basisklasse.
    MyBase.Dispose(disposing)
  End Sub
End Class
```

#### D037: Als je een instantie maakt die `IDisposable` implementeert, roep dan altijd `Dispose` aan

Je hoeft `Dispose` niet aan te roepen op de instantie als je deze in een using-blok gebruikt.

**Zorg ervoor dat `Dispose` altijd wordt aangeroepen, vanuit ofwel:**

- een using-blok
- een finally-blok
- de `Dispose`-implementatie van de klasse zelf (voor private velden).

#### D038: Gebruik het decimal-type om getallen met decimalen weer te geven

De meeste floating-point-waarden hebben geen exacte binaire representatie en hebben een beperkte precisie. Beschouw het volgende voorbeeld waarin twee floats en twee decimals worden vergeleken:

```csharp
// double is een floating-point-formaat
double fa = 3.65, fb = 0.05, fc = 3.7;

// decimal is geen floating-point-formaat
decimal da = 3.65M, db = 0.05M, dc = 3.7M;

fa += fb;
da += db;

Console.WriteLine(fa == fc); // False
Console.WriteLine(da == dc); // True
```

```vbnet
' double is een floating-point-formaat
Dim fa = 3.65, fb = 0.05, fc = 3.7

'decimal is geen floating-point-formaat
Dim da = 3.65d, db = 0.05d, dc = 3.7d

fa += fb
da += db

Console.WriteLine(fa = fc) ' False
Console.WriteLine(da = dc) ' True
```

Uitzonderingen:
- Gebruik een double wanneer de precisie van een decimal onvoldoende is.

### Constanten

#### D039: Gebruik geen letterlijke waarden anders dan 0, 1, `null` (`Nothing` in VB) om constanten te definiëren

Gebruik het volgende patroon om constanten te definiëren:

```csharp
public class Whatever
{
  private static readonly Color _papayaWhip = new Color(0xFFEFD5);
  private const int _daysInWeek = 7;
}
```

```vbnet
Public Class Whatever
    Private Shared ReadOnly _papayaWhip As New Color(&HFFEFD5)
    Private Const _daysInWeek = 7
End Class
```

Er zijn uitzonderingen: de waarden 0, 1 en `null` (`Nothing` in VB) kunnen bijna altijd veilig worden gebruikt. Heel vaak zijn de waarden 2 en -1 ook OK. Strings bedoeld voor logging of tracing zijn vrijgesteld van deze regel. Literals zijn toegestaan binnen een enkele methode wanneer ze alleen van toepassing zijn op die methode en hun betekenis duidelijk is in de context.

mean = (a + b) / 2; //oke

Als de waarde van de ene constante afhangt van de waarde van een andere, probeer dit dan expliciet te maken in de code, dus schrijf niet:

```csharp
public class SomeSpecialContainer
{
  private const int _maxItems = 32;
  private const int _highWaterMark = 24; //op 75%, Fout
}
```

```vbnet
Public Class SomeSpecialContainer
    Private Const _maxItems = 32
    Private Const _highWaterMark = 24 'op 75%, Fout
End Class
```

maar schrijf liever:

```csharp
public class SomeSpecialContainer
{
  private const int _maxItems = 32;
  private const int _highWaterMark = _maxItems * 0.75; // op 75%, Ok
}
```

```vbnet
Public Class SomeSpecialContainer
    Private Const _maxItems = 32
    Private Const _highWaterMark = _maxItems * 0.75 ' op 75%, Ok
End Class
```

Let op dat een *enum* vaak kan worden gebruikt voor bepaalde soorten symbolische constanten. Zorg ervoor dat een constante waarde een echte constante is (zoals natuurlijke constanten, bijv. het aantal dagen in een week of het aantal dagen in een jaar of schrikkeljaar), als een publieke constante wordt gewijzigd, moeten alle aanroepers opnieuw worden gecompileerd. Een item een constante maken zorgt ervoor dat er maar één keer geheugen wordt toegewezen.

### Velden

#### D040: Declareer velden nooit als extern

Velden mogen nooit als extern worden gedeclareerd.

Uitzonderingen:
- `static readonly` velden en constanten, die elke geschikte toegankelijkheid mogen hebben.

Niet-private velden gebruiken geen underscore-prefix, maar worden benoemd als een property (Pascal-cased).

### Methoden

#### D041: Gebruik dynamische binding om onderscheid te maken tussen types

Dit is een algemeen OO-principe. Let op dat het meestal een ontwerpfout is om een selectie-statement te schrijven dat het type van een instantie opvraagt (sleutelwoorden *typeof, is*).

Fout voorbeeld:

```csharp
static string GetSerializedObject(object objectToSerialize)  // Fout
{
  if (objectToSerialize is ISimpleSerialize ss)
  {
    return ss.Serialize();
  }
  else if (objectToSerialize is IAdvancedSerialize as)
  {
    return as.Serialize(System.Threading.Thread.CurrentThread.CurrentCulture);
  }
  else
  {
    throw new ArgumentException("Object not serializeable", "objectToSerialize");
  }
}
```

```vbnet
Sub GetSerializedObject(objectToSerialize As Object) ' Fout
    If TypeOf objectToSerialize Is ISimpleSerialize Then
        Console.WriteLine(CType(objectToSerialize, ISimpleSerialize).Serialize())
    ElseIf TypeOf objectToSerialize Is IAdvancedSerialize Then
        Console.WriteLine(CType(objectToSerialize, IAdvancedSerialize).Serialize(System.Threading.Thread.CurrentThread.CurrentCulture))
    Else
        Throw New ArgumentException("Object not serializeable", "objectToSerialize")
    End If
End Sub
```

Goed voorbeeld:

```csharp
static string GetSerializedObject(ISimpleSerialize objectToSerialize)
{
  return objectToSerialize.Serialize();
}

static string GetSerializedObject(IAdvancedSerialize objectToSerialize)
{
  return objectToSerialize.Serialize(System.Threading.Thread.CurrentThread.CurrentCulture);
}
```

```vbnet
Sub GetSerializedObject(objectToSerialize As ISimpleSerialize)
  Console.WriteLine(objectToSerialize.Serialize())
End Sub

Sub GetSerializedObject(objectToSerialize As IAdvancedSerialize)
  Console.WriteLine(objectToSerialize.Serialize(System.Threading.Thread.CurrentThread.CurrentCulture))
End Sub
```

Uitzonderingen:
- Het gebruik van een selectie-statement om te bepalen of sommige instanties een of meer **optionele** interfaces implementeren, is echter een geldige constructie.

#### D042: Als je de mogelijkheid moet bieden om een methode te overschrijven, maak dan alleen de meest complete overload virtueel en definieer de andere operaties in termen daarvan

Het gebruik van het hieronder gedemonstreerde patroon vereist dat een afgeleide klasse alleen de virtuele methode overschrijft. Aangezien alle andere methoden worden geïmplementeerd door de meest complete overload aan te roepen, zullen ze automatisch de nieuwe implementatie gebruiken die door de afgeleide klasse wordt geleverd.

```csharp
public class MultipleOverrideDemo
{
  private string _someText;

  public MultipleOverrideDemo(string s)
  {
    _someText = s;
  }

  public int IndexOf(string s)
  {
    return IndexOf(s, 0);
  }

  public int IndexOf(string s, int startIndex)
  {
    return IndexOf(s, startIndex, _someText.Length - startIndex);
  }

  public virtual int IndexOf(string s, int startIndex, int count)
  {
    return _someText.IndexOf(s, startIndex, count);
  }
}
```

```vbnet
Class MultipleOverrideDemo
    private _someText As String

    Public Sub New(s As String)
        _someText = s
    End Sub

    Public Function IndexOf(s As String) As Integer
        Return IndexOf(s, 0)
    End Function

    Public Function IndexOf(s As String, startIndex As Integer) As Integer
        Return IndexOf(s, startIndex, _someText.Length - startIndex)
    End Function

    Public Overridable Function IndexOf(s As String, startIndex As Integer, count As Integer) As Integer
        Return _someText.IndexOf(s, startIndex, count)
    End Function
End Class
```

#### D043: Gebruik geen operator-overloading

We raden aan om geen operator-overloading te gebruiken, alleen op primitieve types.

Zie [Microsoft-aanbevelingen](http://msdn.microsoft.com/en-us/library/ms229032\(v=vs.110\).aspx) over dit onderwerp.

#### D044: Wanneer een property te gebruiken in plaats van een methode

Een property moet worden overwogen wanneer:

- Properties zich moeten gedragen alsof het velden zijn.
- Het lid een logisch datalid is.
- De operatie geen conversie is.
- De operatie niet duur is. De gebruiker hoeft niet te overwegen de resultaten te cachen.
- Het verkrijgen van een property-waarde met de *get*-accessor geen waarneembaar neveneffect zou hebben.
- Het twee keer achter elkaar aanroepen van het lid dezelfde resultaten oplevert.
- Properties stateless moeten zijn ten opzichte van andere properties, d.w.z. er mag geen waarneembaar verschil zijn tussen eerst property A en dan B instellen en andersom.

Overweeg bij het gebruik van properties een propertychanged-event te gebruiken in plaats van een changed-event voor elke property. Ook mogen properties alleen in zeer uitzonderlijke gevallen fouten retourneren. In een normaal scenario mag een property geen fout retourneren. Als een property alleen een zinnig getal kan retourneren nadat een bepaalde methode is uitgevoerd, retourneer dan een standaardwaarde of `null` totdat de methode is aangeroepen. Retourneer geen fout.

#### D045: Gebruik geen properties om een array van waarden op te halen of in te stellen

Dit zorgt ervoor dat property-waarden buiten de bevattende klasse kunnen worden gewijzigd. Stel de waarden bloot als een readonly type (bevat data in een readonly type) of gebruik een methode die een kloon van de data blootstelt. Stel geen IEnumberable<> van dezelfde array bloot, omdat deze gewoon terug kan worden gecast naar de array en kan worden gewijzigd.

```csharp
class MyClass
{
    private string[] _data;

    // Fout, resultaat kan worden gecast naar string[]
    public IEnumerable<string> Data
    {
        get { return _data; } // Fout, het gebruik van _data.GetEnumerator() zou dit oplossen
    }

    // Ok, maar dit kan traag zijn, dus het zou een methode moeten zijn in plaats van een property
    public string[] GetData()
    {
        return (string[])_data.Clone();
    }

    // Goed, resultaat kan alleen worden gelezen
    public ReadOnlyCollection<string> Data
    {
        get { return Array.AsReadOnly(_data); }
    }
}
```

```vbnet
Class MyClass
    Private _data As String()

    ' Fout, resultaat kan worden gecast naar string[]
    Public ReadOnly Property Data As IEnumerable(Of String)
        Get
            Return _data ' Fout, het gebruik van _data.GetEnumerator() zou dit oplossen
        End Get
    End Property

    ' Ok, maar dit kan traag zijn, dus het zou een methode moeten zijn in plaats van een property
    Public Function GetData() As String()
        Return _data.Clone()
    End Function

    ' Goed, resultaat kan alleen worden gelezen
    Public ReadOnly Property Data As ReadOnlyCollection(Of String)
        Get
            Return Array.AsReadOnly(_data)
        End Get
    End Property
End Class
```

### Collecties

#### D046: Retourneer geen `null` (`Nothing` in VB) van niet-private properties of methoden om een lege collectie of IEnumerable aan te duiden

Retourneer altijd een leeg collectietype in plaats van `null` (`Nothing` in VB), om het gebruik van Linq en andere methoden te vergemakkelijken zonder de noodzaak om elke collectie op `null` te controleren. Dit voorkomt ook moeilijk te vinden bugs.

```csharp
private Node[] _children;

// Fout, retourneert null wanneer _children null is
public IEnumerable<Node> Children
{
  get { return _children?.GetEnumerator(); }
}

// OK
public IEnumerable<Node> Children
{
  get { return _children?.GetEnumerator() ?? Array.Empty<Node>(); }
}
```

```vbnet
private _children As Node()

' Fout, retourneert nothing wanneer _children nothing is
public ReadOnly Property Children As IEnumerable(Of Node)
    Get
        Return _children?.GetEnumerator()
    End Get
End Property

' Ok
public ReadOnly Property Children As IEnumerable(Of Node)
    Get
        Return If(_children?.GetEnumerator(), Array.Empty(Of Node()))
    End Get
End Property
```

### Namespace

#### D047: Benoem namespaces volgens een vooraf gedefinieerd patroon en logische feature

\<Bedrijf>.(\<Product>|\<Technologie>)[.\<Feature>][.\<Subnamespace>]

- Voorzie namespace-namen van een bedrijfsnaam om te voorkomen dat namespaces van verschillende bedrijven dezelfde naam hebben.
- Gebruik een stabiele, versie-onafhankelijke productnaam op het tweede niveau van een namespace-naam.
- Gebruik GEEN organisatorische hiërarchieën als basis voor namen in namespace-hiërarchieën, omdat groepsnamen binnen bedrijven de neiging hebben van korte duur te zijn. Organiseer de hiërarchie van namespaces rond groepen gerelateerde technologieën.
- Geef waar van toepassing de voorkeur aan meervoudige namespace-namen. Gebruik bijvoorbeeld System.Collections in plaats van System.Collection. Merknamen en acroniemen zijn echter uitzonderingen op deze regel. Gebruik bijvoorbeeld System.IO in plaats van System.IOs.
- Gebruik NIET dezelfde naam voor een namespace en een type in die namespace. Dit resulteert in moeilijk te gebruiken types.
- Overweeg dezelfde structuur te gebruiken als Microsoft voor hun namespaces. Dit maakt het voor ontwikkelaars gemakkelijker om de code te vinden.

### Assemblies

#### D048: Benoem assemblies volgens een vooraf gedefinieerd patroon en de grote brokken functionaliteit

Begin alle assembly-namen met \<Bedrijf>.\<Product>. Scheid subdelen met een punt(.). Bijvoorbeeld: Microsoft.Office.Excel.

Kies namen voor je assembly die grote brokken functionaliteit suggereren, zoals System.Data.

Assemblynamen hoeven niet overeen te komen met namespace-namen, maar het is redelijk om de namespace-naam te volgen bij het benoemen van assemblies. Een goede vuistregel is om de assembly te benoemen op basis van het gemeenschappelijke voorvoegsel van de namespaces in de assembly. Bijvoorbeeld, een assembly met twee namespaces, MyCompany.MyTechnology.FirstFeature en MyCompany.MyTechnology.SecondFeature, zou MyCompany.MyTechnology.dll kunnen heten.

## OO-principes

#### D049: Het moet mogelijk zijn om een verwijzing naar een instantie van een afgeleide klasse te gebruiken waar een verwijzing naar de basisklasse van die instantie wordt gebruikt

Deze regel staat bekend als het Liskov Substitution Principle, vaak afgekort tot LSP. Let op dat een interface in deze context ook als een basisklasse wordt beschouwd.

#### D050: Alle varianten van een overbelaste methode moeten voor hetzelfde doel worden gebruikt en vergelijkbaar gedrag hebben

Anders handelen is in strijd met het Principle of Least Surprise. Als algemene regel moet je alleen overloads gebruiken als de overloads elkaar aanroepen en geen extra gedrag hebben.

Voorbeeld:

```csharp
private void MakePageActive(Page page)
{
  page.Visible = true;
  page.TopMost = true;
}

private void MakePageActive(int pageNumber)
{
  for (int i = 0; i < Pages.Length; i++)
  {
    if (i = pageNumber)
    {
      MakePageActive(Pages[i]);
    }
    else
    {
      Pages[i].Visible = false; // Fout, vanwege dit extra gedrag
    }
  }
}
```
```vbnet
Sub MakePageActive(page As Page)
    page.Visible = True
    page.TopMost = True
End Sub

Sub MakePageActive(pageNumber As Integer)
    For i As Integer = 0 To Pages.Length - 1
        If i = pageNumber Then
            MakePageActive(Pages(i))
        Else
            Pages(i).Visible = False
        End If
    Next
End Sub
```

# Onderhoudbaarheid

## Algemeen

#### M001: Meng geen code van verschillende providers in één bestand

Over het algemeen zal code van derden (bijv. code gegenereerd door Visual Studio) niet voldoen aan de codeerstandaarden, dus plaats dergelijke code niet in hetzelfde bestand als eigen code of zorg ervoor dat deze voldoet.

#### M002: Declareer en initialiseer variabelen dicht bij waar ze worden gebruikt

Dit is een best practice zodat code gemakkelijk kan worden gerefactord of opgesplitst in nieuwe functies.

Uitzonderingen:
- Dit geldt niet voor variabelen op klasseniveau.

#### M003: Initialiseer variabelen indien mogelijk op het punt van declaratie

Als je veldinitialisatie gebruikt, worden instance-velden geinitialiseerd voordat de instance-constructor wordt aangeroepen. Evenzo worden `static` velden geinitialiseerd wanneer de `static` constructor wordt aangeroepen of voor het eerste gebruik. Merk op dat de compiler elke niet-geinitialiseerde waardetype-variabele altijd op nul zal initialiseren en referentietype-variabele op `null` (`Nothing` in VB). Merk ook op dat het perfect is toegestaan om de ? : operator (if(x,y,z) in VB) te gebruiken.

```csharp
enum Color
{
  Red,
  Green
}

bool _fatalSituation = IsFatalSituation();
Color _backgroundColor = _fatalSituation ? Color.Red : Color.Green;
```

```vbnet
Enum Color
    Red
    Green
End Enum

Dim _fatalSituation = IsFatalSituation()
Dim _backgroundColor = If(_fatalSituation, Color.Red, Color.Green)
```

#### M004: Benader een gewijzigde instantie niet meer dan eens in een expressie (alleen C#)

De evaluatievolgorde van subexpressies binnen een expressie is gedefinieerd, maar dergelijke code is moeilijk te begrijpen.

Voorbeeld:

```csharp
int x = 0, y = 0, z = 0;
var list = new List<int>() { x, y };

list[x] = x++; // Fout, moeilijk te lezen (wordt list[0] of list[1] ingesteld en met 1 of 2)
y = y++ + 2; // Fout, moeilijk te lezen (is y 2 of 3), overweeg y += 2 te gebruiken
z = ++z + 2; // Fout, moeilijk te lezen (is z 2 of 3), overweeg z += 3 te gebruiken
```

## Inspringen en afbreken

#### M005: Gebruik twee spaties inspringing voor codebestanden

Gebruik twee spaties inspringing (geen tabs).

#### M006: Lijn functieargumenten links uit bij het afbreken van de functieaanroep

Functieaanroepen kunnen worden afgebroken als ze te groot worden. Breek altijd rechts van het haakje van de vorige regel af. Lijn bij het afbreken van een functieaanroep de argumenten links uit:

```csharp
CreateFile(configFilePath, overwrite,
           encoding);
```

```vbnet
CreateFile(configFilePath, overwrite, _
           encoding);
```

Uitzonderingen:
- Als er nog steeds geen ruimte is vanwege de lengte van de functienaam, gebruik dan twee inspringingen (4 spaties) ten opzichte van de regel die de statement begint.

#### M007: Lijn afgebroken voorwaarden links uit

Samengestelde voorwaarden kunnen worden afgebroken als ze te groot worden. Spring de nieuwe regel altijd in met twee inspringingen (4 spaties) ten opzichte van de regel die de statement begint. Begin de nieuwe regel met een binaire operator:

```csharp
if (!File.Exists(filePath) || thisIsABool
    || overwrite)
{
}
```

```vbnet
If !File.Exists(filePath) || thisIsABool _
    || overwrite Then
  ...
End If
```

## .NET Taalconstructies

#### M008: Gebruik nooit `this` (`Me` in VB)

Gebruik nooit de *this*. (Me. in VB) constructie, behalve als het nodig is om types van leden te onderscheiden.

#### M009: Maak geen expliciete vergelijkingen met true of false

Het is meestal slechte stijl om een bool-type expressie te vergelijken met true of false.

Voorbeeld:

```csharp
while (condition == false) // Fout; slechte stijl
while (condition != true) // ook Fout
while (((condition == true) == true) == true) // waar stop je? (Fout)
while (condition)  // OK
while (!condition) // OK
```

```vbnet
While condition = false ' Fout; slechte stijl
While condition <> true ' ook Fout
While (((condition = true) = true) = true) ' waar stop je? (Fout)
While condition  ' OK
While Not condition ' OK
```

#### M010: Gebruik geen LINQ Query-taalsyntaxis

Gebruik de methodesyntaxis in plaats van de querysyntaxis. De methodesyntaxis is flexibeler en kan in meer scenario's worden gebruikt. De querysyntaxis is slechts een wrapper om de methodesyntaxis.

Voorbeeld:

```csharp
var query = from c in customers
            where c.City == "London"; // Fout

var query = customers.Where(c => c.City == "London"); // OK
```

```vbnet
Dim query = From c In customers
            Where c.City = "London" ' Fout

Dim query = customers.Where(Function(c) c.City = "London") ' OK
```

#### M011: Hergooi zonder parameters om stackdetails te behouden

Zodra een exception wordt gegooid, is een deel van de informatie die het meedraagt de stack trace. De stack trace is de lijst van de methods die begint met de methode die de exception gooit en eindigt met de methode die de exception opvangt. Als een exception opnieuw wordt gegooid door de exception in de throw-statement te specificeren, wordt de stack trace opnieuw gestart bij de huidige methode en gaat de lijst met methode-aanroepen tussen de oorspronkelijke methode die de exception gooide en de huidige methode verloren. Om de oorspronkelijke stack trace-informatie bij de exception te houden, gebruik je de throw-statement zonder de exception te specificeren.

```csharp
try
{
  throw new MsgException("error");
}
catch(MsgException ex)
{
  //DoStuff
  throw ex; // Fout, gebruik de exception niet als parameter bij het opnieuw gooien.
}

try
{
  throw new MsgException("error");
}
catch(MsgException ex)
{
  //DoStuff
  throw; // Ok
}
```

```vbnet
Try
    Throw New MsgException("error")
Catch ex As MsgException
    'DoStuff
    Throw ex '  Fout, gebruik de exception niet als parameter bij het opnieuw gooien.
End Try

Try
    Throw New MsgException("error")
Catch ex As MsgException
    'DoStuff
    Throw ' Ok
End Try
```

#### M012: Gebruik geen expliciete Type-declaratie voor Generieke methoden die een parameter van hetzelfde generieke type hebben

Generieke methoden die generieke parameter(s) hebben van hetzelfde generieke type dat wordt gebruikt in de declaratie van de methoden, kunnen worden gebruikt zonder expliciet het generieke type te specificeren; dit type wordt afgeleid van de waarde die aan de parameter wordt gegeven.

```csharp
public void Add<T>(T itemToAdd) { } // Generieke methode.

int i = 9;
Add<int>(i);     // Fout, expliciete type-declaratie wordt gebruikt.
Add(i);          // Ok, geen expliciete type-declaratie, type wordt afgeleid van de opgegeven parameter.
```

```vbnet
Sub Add(Of T)(item As T)
End Sub

Dim i = 0
Add(Of Integer)(i)  ' Fout, expliciete type-declaratie wordt gebruikt.
Add(i)              ' Ok, geen expliciete type-declaratie, type wordt afgeleid van de opgegeven parameter.
```

#### M013: Geef de voorkeur aan `||` boven `|` (`OrElse` boven `Or` in VB) en `&&` boven `&` (`AndAlso` boven `And` in VB)

De || en && zijn kortsluitingsoperatoren. Dit kan een prestatiewinst opleveren.

Voorbeeld:

```csharp
If (a != null && a.Value == 5)
  'Doe iets
End If
```

```vbnet
If a IsNot Nothing AndAlso a.Value = 5 Then
  'Doe iets
End If
```

De &-operator zou beide expressies evalueren, zelfs als de eerste expressie False is. De &&-operator zou de tweede expressie alleen evalueren als de eerste expressie True is. Dit voorkomt ook NullReferenceExceptions.

#### M014: Gebruik de voorkeursmethoden voor stringvergelijking en sortering

- DO: Gebruik `StringComparison.Ordinal` of `OrdinalIgnoreCase` voor vergelijkingen als je veilige standaard voor cultuur-agnostische string-matching.
- DO: Gebruik `StringComparison.Ordinal` en `OrdinalIgnoreCase` vergelijkingen voor verhoogde snelheid.
- DO: Gebruik StringComparison.CurrentCulture-gebaseerde stringoperaties bij het weergeven van de uitvoer aan de gebruiker.
- DO: Schakel het huidige gebruik van stringoperaties op basis van de invariante cultuur over naar het gebruik van de niet-linguïstische `StringComparison.Ordinal` of `StringComparison.OrdinalIgnoreCase` wanneer de vergelijking linguïstisch irrelevant is (symbolisch, bijvoorbeeld).
- DO: Gebruik `ToUpperInvariant` in plaats van `ToLowerInvariant` bij het normaliseren van strings voor vergelijking.
- DON'T: Gebruik overloads voor stringoperaties die niet expliciet of impliciet het stringvergelijkingsmechanisme specificeren.
- DON'T: Gebruik in de meeste gevallen `StringComparison.InvariantCulture`-gebaseerde stringoperaties; een van de weinige uitzonderingen zou het persisteren van linguïstisch betekenisvolle maar cultuur-agnostische data zijn.

`String.Compare` toont je intentie beter dan `ToUpper`. `ToUpper` is sneller dan `ToLower`. Ook gebruiken sommige talen (culturen) andere hoofdlettergebruik en sortering dan andere, bijvoorbeeld de &#223; vs ss in het Duits en het Turkse-I-probleem. Dus het specificeren van Ordinal, Invariant of culture zal een betere intentie in de code tonen.

Dus een naam met een &#223; wordt op een andere positie getoond in een Duitse applicatie (bijvoorbeeld een telefoonboek) dan in een Nederlandse applicatie.

#### M015: Gebruik XML-commentaar voor het beschrijven van methoden, klassen, velden en alle publieke leden

Kan worden opgepikt door tools zoals Visual Studio om intellisense en helpgeneratie te bieden.

#### M016: Zorg voor schone commentaren

- Gebruik single-line commentaar (//) voor korte uitleg.
- Vermijd multi-line commentaar (/* */) voor langere uitleg. Commentaar wordt niet gelokaliseerd. In plaats daarvan staan langere uitleg in het bijbehorende artikel.
- Begin commentaartekst met een hoofdletter.
- Eindig commentaartekst met een punt.
- Voeg een spatie in tussen de commentaardelimiter (//) en de commentaartekst, zoals getoond in het volgende voorbeeld.

```csharp
// De volgende declaratie maakt een query. Het voert
// de query niet uit.
```

```vbnet
' De volgende declaratie maakt een query. Het voert
' de query niet uit.
```

## Alleen C# Taalconstructies

#### M017: Gebruik een directe cast als je weet dat de cast nooit mislukt, anders gebruik je `as` of `is`

Als je as gebruikt, moet je altijd op `null` controleren.

Voorbeeld:

```csharp
var c = (ICalc)RemotingServices.Connect(typeof(ICalc), @"http://localhost:999/demo");  //Ok omdat het returntype bekend is

bool valid = true;

foreach(Control ctrl in Controls)
{
  var actrl = ctrl as AzureControl; //gebruik as omdat de cast kan mislukken

  if(actrl != null)
  {
    valid = actrl.IsValid;
    break;
  }
}
```

#### M018: Een blok moet altijd op een nieuwe regel beginnen. Een statement-einde (\;) mag nooit worden gevolgd door een nieuwe statement op dezelfde regel.

Dit verhoogt de leesbaarheid en kan soms onbedoelde fouten voorkomen die worden veroorzaakt door onnauwkeurige knip- en plakoperaties, vanwege een bungelende accolade.

Let op dat dit ook mogelijke verwarring voorkomt in statements van de vorm:

```csharp
// Fout
if (b1) Foo(); else Bar(); 

// Fout
if(b1) { Foo(); } else Bar(); 

// OK
if(b1)
{ 
  Foo();
} 
else
{
  Bar();
}
```

#### M019: Geef de voorkeur aan `is null` boven `== null` en `is not null` boven `!= null` bij expliciete vergelijking met `null`

De == en != zijn `virtual` operatoren, de is-operator niet. De is-operator is een taalconstructie.

Let op dat dit alleen van toepassing is bij expliciete vergelijking en niet wanneer we variabele waarden vergelijken.

```csharp
if(x is null) // Ok
{
  if(y == null) // Fout, is null zou de voorkeur hebben
  {
    if(x == y) // Ok, variabele vergelijking
    { }
  }
}
```

#### M020: Schakel de nullable reference types compilermodus in

`NullReferenceException` is een van de meest voorkomende runtime-exceptions. De Nullable-compilermodus stelt je in staat specifieker te zijn over je intentie en vereist dat je de `!`-operator gebruikt wanneer je weet dat een referentietype nooit `null` kan zijn. Stel WarningsAsErrors altijd in op Nullable zodat deze waarschuwingen moeten worden opgelost voordat de applicatie wordt uitgevoerd.

Je kunt de nullable context tijdelijk uitschakelen in een (deel van een) bestand door gebruik te maken van:

```csharp
#nullable enable //Schakelt nullable context in.
#nullable disable // Schakelt nullable context uit.
#nullable restore //Herstelt de nullable context naar de projectstandaard.
```

Merk op dat nullable reference types een compiler-feature is. Op .NET-niveau ondersteunen alle klassen nog steeds null en er is geen ander type voor nullable reference types, zoals er wel is voor nullable structs (Nullable\<T>). Dus tijdens runtime worden er geen regels afgedwongen en talen zonder Nullable reference types (bijvoorbeeld VB.NET) kunnen sommige waarden op `null` instellen, dus voor referentietypes moet je nog steeds null-controles implementeren.

Stel de volgende opties in je projectbestand (of Directory.Build.props) in.

```xml
<Nullable>enable</Nullable>
<WarningsAsErrors>Nullable</WarningsAsErrors>
```

Meer info:
https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references
https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/errors-warnings

## Alleen VB.NET Taalconstructies

#### M021: Gebruik DirectCast in plaats van CType()

`DirectCast` is sneller en specifieker dan `CType`, en het laat je ook geen precisie verliezen.

```vbnet
Dim newint As Integer = DirectCast(3345.34, Integer) 'Compilerfout: Option Strict On staat impliciete conversies van 'Double' naar 'Integer' niet toe.
Dim newint2 As Integer = CType(3345.34, Integer) 'Compileert wel
```

Gebruik `DirectCast` als je zeker weet dat de cast zal slagen, anders gebruik je `TryCast()` en controleer je op `Nothing`.

#### M022: Schakel Option Strict, Option Explicit en Option Infer in

Schakel altijd Option Strict, Option Explicit en Option Infer in in de projectinstellingen. Dit voorkomt veel fouten en maakt de code leesbaarder.

```xml
<OptionExplicit>On</OptionExplicit>
<OptionInfer>On</OptionInfer>
<OptionStrict>On</OptionStrict>
```

Meer info:

https://learn.microsoft.com/en-us/dotnet/visual-basic/language-reference/statements/option-explicit-statement
https://learn.microsoft.com/en-us/dotnet/visual-basic/language-reference/statements/option-infer-statement
https://learn.microsoft.com/en-us/dotnet/visual-basic/language-reference/statements/option-strict-statement

#### M023: Gebruik geen On Error Goto

Gebruik de Try...Catch en Using Statements wanneer je Exception Handling gebruikt.

#### M024: Gebruik het IsNot-sleutelwoord

Gebruik `IsNot Nothing` in plaats van `Not ... Is Nothing`.

#### M025: Gebruik geen `static` variabelen op methodeniveau

Geef de voorkeur aan variabelen op klasseniveau. `static` variabelen op methodeniveau worden vaak verkeerd begrepen, wat leidt tot moeilijk te vinden bugs.

```vbnet
Private Sub DoSomething()
  Static counter As Integer 'Fout, gebruik een variabele op klasse-/moduleniveau

  ...
End Function
```

## Broncodebeheer

#### M026: Verplaats of wijzig geen codebases op basis van persoonlijke voorkeur

Broncodebeheer voegt wijzigingen samen (merge) op basis van delta's die toegevoegde en verwijderde regels bevatten. Ze detecteren echter geen verplaatsingen. Het verplaatsen van regels verhoogt de kans op merge-conflicten en voorkomt auto-merge. Het maakt het moeilijker om wijzigingen in de loop van de tijd te volgen met behulp van de geschiedenisweergave. 

Verander identifiers niet op basis van persoonlijke voorkeur, maar verander ze wel om typefouten te corrigeren of als de inhoud van de identifier de naam niet meer goed weergeeft. Bespreek bij twijfel de voorgestelde wijziging met ten minste een andere (lead) ontwikkelaar.

#### M027: Push je code frequent

Hoe eerder je je wijzigingen pushed, hoe eerder anderen je code kunnen mergen. Dit voorkomt merge-conflicten. Je hoeft niet te wachten tot alles werkt voordat je pushed. Zolang je de bestaande functionaliteit niet breekt en de code compileert, kun je pushen. In feature branches kun je zelfs bestaande functionaliteit breken, zolang je communiceert wat je breekt met andere teamleden die in de branch werken. Vuistregel: Dagelijks, maar minstens één keer per week.

#### M028: Wanneer verplaatsingen of hernoemingen deel uitmaken van de check-in, let dan goed op dat dit correct in de commit staat

Als dit niet goed is geregisteerd dan ziet men dit als een delete en add. Hierdoor worden andere wijzigingen van programeurs mogelijke genegeerd en kan dit merge-conflicten en bugs veroorzaken die zeer moeilijk op te lossen zijn. Controleer je commit om er zeker van te zijn dat je wijziging correct is geregisteerd in Broncodebeheer.

#### M029: Voeg nieuwe functies toe onder de functie die ze aanroept

Dit maakt wijzigingen beter leesbaar en voorkomt merge-conflicten. De kans dat twee gebruikers dezelfde functie herschrijven is veel kleiner dan de kans dat twee gebruikers functies aan hetzelfde bestand toevoegen.

#### M030: Plan hernoemingsrefactoring met grote codewijzigingen en merge ze afzonderlijk naar master

Als je een kleine wijziging hebt die veel bestanden beïnvloedt, breng die wijziging dan afzonderlijk van andere wijzigingen uit. Voorbeeld: als je de naam van een functie, klasse wilt wijzigen of een klasse naar een andere namespace wilt verplaatsen en je merkt dat dit veel bestanden beïnvloedt, nemen de kansen en complexiteit van merge-conflicten exponentieel toe. Je kunt ervoor kiezen om de naamswijziging direct naar master uit te brengen mede programmeurs te informeren zo snel mogelijk te pullen. Dit stelt alle andere eigenaren in staat om de wijziging te mergen en de relatief eenvoudige merge-conflicten op te lossen zonder te maken te hebben met de daadwerkelijke programma-structuurwijzigingen. Een andere optie is om de hernoeming uit te stellen totdat je branch is gemerged naar master en dan de naamswijziging uit te voeren als onderdeel van een afzonderlijke commit naar master.

#### M031: Als je een fix doorvoerd, overweeg dan sterk om documentatie in de broncode toe te voegen

Het feit dat je een bug hebt gemaakt, betekent waarschijnlijk dat je een scenario niet hebt overwogen of dat de constructie complexer was dan je aanvankelijk dacht. Je moet dat scenario in de code documenteren of wat commentaar toevoegen dat de gerepareerde regels in meer detail beschrijft. Dit voorkomt dat je het scenario vergeet bij de volgende fix en stelt anderen die de code lezen in staat om dezelfde kennis te hebben.

# Prestaties

## .NET

#### P001: Gebruik een `StringBuilder` voor het samenstellen van strings in iteraties

Strings zijn onveranderlijk (immutable), wat betekent dat wanneer je twee strings aan elkaar koppelt, je in feite een nieuwe string maakt en de inhoud van de andere twee erin kopieert. Hoe meer strings worden samengevoegd, hoe meer er wordt gekopieerd, wat kan leiden tot een dramatisch prestatieverlies. Wanneer een string in een soort iteratie wordt samengesteld, maak dan een StringBuilder-instantie om de string te construeren:

```csharp
private static string GetSqlSelect(Fields fields)
{
  StringBuilder sql = new StringBuilder("SELECT ");

  foreach(Field field in fields)
  {
    sql.Append(field.Name);
    sql.Append(",")
  }

  return sql.ToString(0, sql.Length - 1);
}
```
```vbnet
Private Function GetSqlSelect(fields As Fields) As String
    Dim sql As New StringBuilder("SELECT ")

    For Each field As Field In fields
        sql.Append(field.Name)
        sql.Append(",")
    Next

    Return sql.ToString(0, sql.Length - 1)
End Function
```

Overweeg bij het samenstellen van een string van elementen in een string-array de statische *string.Join*-methode te gebruiken:

```csharp
private static string GetSqlSelect(string[] fields)
{
  return $"SELECT {string.Join(",", fields)}";
}
```

```vbnet
Private Function GetSqlSelect(fields As String()) As String
    Return $"SELECT {String.Join(",", fields)}"
End Function
```

#### P002: Gebruik string-interpolatie om string-constanten, variabelen of literals samen te voegen

Gebruik bij het samenvoegen van string-constanten, variabelen of literals string-interpolatie voor de beste prestaties. Bijvoorbeeld:

```csharp
public static string GetErrorMessage(string message)
{
  return $"Error '{message}' occurred at {DateTime.Now:G}";
}
```

```vbnet
Public Function GetErrorMessage(message As String) As String
    Return $"Error '{message}' occurred at {DateTime.Now:G}"
End Function
```

#### P003: Gooi alleen exceptions in uitzonderlijke gevallen

Je moet alleen exceptions gooien wanneer er een voorwaarde optreedt die buiten de aannames van je code valt. Met andere woorden, je moet exceptions niet gebruiken als een middel om je beoogde functionaliteit te bieden. Een gebruiker kan bijvoorbeeld een ongeldige gebruikersnaam of wachtwoord invoeren bij het inloggen op een applicatie. Hoewel dit geen succesvolle aanmelding is, zou het een geldig en verwacht resultaat moeten zijn en daarom geen exception moeten gooien. Er moet echter een exception worden gegenereerd als er een onverwachte voorwaarde optreedt, zoals een niet-beschikbare gebruikersdatabase. Het gooien van exceptions is duurder dan simpelweg een resultaat teruggeven aan een aanroeper. Daarom moeten exceptions niet worden gebruikt om de normale programmastroom te beheersen. Bovendien kan overmatig gebruik van exceptions onleesbare en onbeheerbare code creëren.

## WPF

# UI - UX

## WPF

#### U001: Designer-types moeten worden voorzien van het type als achtervoegsel

Designer-types mogen worden voorzien van het Designer-type als achtervoegsel. Designer-types mogen niet met een underscore beginnen. Als de naam erg lang wordt, zijn afkortingen toegestaan.

Voorbeelden:
```
_nameTextBox // Fout, begint met _
textBoxName // Fout, voorafgegaan door designer-type
nameTextBox // OK

isInvoicingSelectedDataGridCheckBoxColumn // OK
```

#### U002: Schakel Snaptodevicepixels in op alle formulieren

Stel dit in op true op het root-element van elk formulier. Voor apparaten die werken met meer dan 96 dots per inch (dpi), kan pixel-snap-rendering visuele anti-aliasing-artefacten minimaliseren in de buurt van enkelvoudige ononderbroken lijnen.

#### U003: Stel `RenderOptions.BitmapScalingMode` in op `NearestNeighbor` voor scherpere pictogram- en afbeeldingsranden

Om de scherpte van afbeeldingen te verbeteren.

#### U004: Stel `TextOptions.TextFormattingMode` in op `Display` wanneer de lettergrootte \<= 15 is

Om de tekstscherpte bij kleine lettertypen te verbeteren.

#### U005: Stel `VirtualizingStackPanel.VirtualizationMode` in op `Recycling` wanneer de visuele boom een `VirtualizingStackPanel` bevat

Standaard maakt een VirtualizingStackPanel een itemcontainer voor elk zichtbaar item en gooit deze weg wanneer deze niet langer nodig is (zoals wanneer het item uit het zicht wordt gescrold). Wanneer een ItemsControl veel items bevat, kan het proces van het maken en weggooien van itemcontainers de prestaties negatief beïnvloeden. Wanneer VirtualizingStackPanel.VirtualizationMode is ingesteld op Recycling, hergebruikt de VirtualizingStackPanel itemcontainers in plaats van elke keer een nieuwe te maken.

#### U006: Stel `x:SynchronousMode` in op `Async` bij het laden van grote xaml

Dit verbetert de laadtijd. Zie [XamlReader.LoadAsync Method](https://msdn.microsoft.com/en-us/library/aa346593\(v=vs.100\).aspx)

#### U007: Stel Binding `IsAsync` in bij het laden van trage properties

Dit maakt laden mogelijk zonder de UI-thread te bevriezen.

#### U008: Geef de voorkeur aan `StaticResources` boven `DynamicResources`

StaticResources bieden waarden voor elk XAML-propertyattribuut door een verwijzing naar een reeds gedefinieerde resource op te zoeken. Het opzoekgedrag voor die resource is hetzelfde als een compile-time opzoeking. DynamicResources maken een tijdelijke expressie en stellen het opzoeken van resources uit totdat de gevraagde resourcewaarde nodig is. Het opzoekgedrag voor die resource is hetzelfde als een run-time opzoeking, wat een prestatie-impact heeft. Gebruik altijd een StaticResource waar mogelijk.

#### U009: Opacity op Brushes in plaats van op Elementen

Als je een `Brush` gebruikt om de `Fill` of `Stroke` van een element in te stellen, is het beter om de `Opacity` op de Brush in te stellen in plaats van de `Opacity`-property van het element. Wanneer je de `Opacity`-property van een element wijzigt, kan dit ervoor zorgen dat WPF tijdelijke oppervlakken creëert, wat resulteert in een prestatieverlies.

#### U010: Geef de voorkeur aan `StreamGeometries` boven `PathGeometries`

Het `StreamGeometry`-type is een zeer lichtgewicht alternatief voor een `PathGeometry`. `StreamGeometry` is geoptimaliseerd voor het verwerken van veel `PathGeometry`-types. Het verbruikt minder geheugen en presteert veel beter in vergelijking met het gebruik van veel `PathGeometry`-types.

#### U011: Gebruik en bevries `Freezable`

Een `Freezable` is een speciaal type dat twee toestanden heeft: ontdooid en bevroren. Wanneer je een instantie zoals een `Brush` of `Geometry` bevriest, kan deze niet langer worden gewijzigd. Het bevriezen van instanties waar mogelijk verbetert de prestaties van je applicatie en vermindert het geheugenverbruik.

#### U012: Repareer je Binding-fouten

Binding-fouten zijn het meest voorkomende type prestatieprobleem in WPF-apps. Elke keer dat er een binding-fout optreedt, krijgt je app een prestatieklap omdat het probeert de binding op te lossen en de fout naar het trace-log schrijft. Zoals je je kunt voorstellen, hoe meer binding-fouten je hebt, hoe groter de prestatieklap die je app zal krijgen. Neem de tijd om al je binding-fouten te vinden en te repareren. Het gebruik van een `RelativeSource`-binding in DataTemplates is een belangrijke boosdoener bij binding-fouten, omdat de binding meestal pas correct wordt opgelost nadat de DataTemplate zijn initialisatie heeft voltooid. Vermijd het gebruik van `RelativeSource.FindAncestor` ten koste van alles. Definieer in plaats daarvan een attached property en gebruik property-inheritance om waarden door de visuele boom naar beneden te duwen in plaats van de visuele boom op te zoeken.
