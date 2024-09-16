We gaan een strongly typed AntaDataBuffer maken.
Bekijk de types `AntaField` en `PersonRow`.

# Opgave 1:
Open het `AntaDataBuffer` type. Dit type moet door strongly typed rows kunnen lopen. Het type van de row moet aan AntaDataBuffer worden meegegeven (T). In dit voorbeeld is dat type `PersonRow`.
De volgende methodes moeten public mogelijk worden: het aantal regels opvragen, leegmaken, regels toevoegen en regels verwijderen. Men mag echter niet gepositioneerd toevoegen/verwijderen/zoeken. Zorg dat deze functies geimplementeerd worden.
Haal het commentaar om het blok "Commentaar opgave 1" weg zodat de code getest kan worden.

# Opgave 2:
We willen nu dat men in de runner door middel van `persons[Guid]` direct een rij kan prikken (alleen get, geen setter).
Dit kunnen we doen doordat we weten dat alle types die we als row kunnen gebruiken altijd een AntaField van het type `Guid` met de naam Id bevatten.
Leg dit gegeven vast in de een gemeenschappelijk type (base class of interface) en implementeer de indexer op AntaDataBuffer.
Haal het commentaar om het blok "Commentaar opgave 2" weg zodat de code getest kan worden.

# Opgave 2 extra:
We willen aan functies een readonly variant van `PersonRow` mee kunnen geven. Dus waarbij de values van de velden niet aan te passen zijn. Hoe zou je dit opzetten? Implementeer een `AsReadOnly` functie op `PersonRow`.