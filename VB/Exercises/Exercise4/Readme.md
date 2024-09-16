We willen uit een grote xml file alle personen lezen en gestreamed onze databuffer vullen.
Een voorbeeld is meegeleverd om dit niet gestreamed te doen.

# Opgave 1:
Run de code tenminste 2x en noteer het aantal seconden en kb's die de eerste `PrintStat` retourneert. Pas het commentaar aan in de runner en implementeer de `StreamXml` functie. De eerste `PrintStat` zou nu vrijwel geen geheugen moeten innemen.

# Opgave 2:
We willen de gevulde databuffer nu wegschrijven als json. Gebruik een `Utf8JsonWriter` i.c.m. `New JsonObject` om deze op te bouwen en weg te schrijven. Hij zou er als volgt uit moeten zien:

```json
[
  {
    "Id": "..",
    "FirstName": "..",
    "LastName": ".."
  }, ..
```

# Opgave 3:
Maak commentaar van de regel `Dim elements = StreamXml(XmlFileName)`. Voeg nieuwe code toe die de data vanuit de zojuist aangemaakte json i.p.v. de xml inleest.

# Opgave 4 extra:
Kijk waar we veel geheugen gebruiken en hoe je dit kan optimaliseren door efficiëntere code. Denk hierbij aan `XStreamingElement`, flushes van de json writer en `JsonSerializer.DeserializeAsyncEnumerable`.