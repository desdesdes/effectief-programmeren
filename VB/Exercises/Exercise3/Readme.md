Bij het type AntaDataBuffer hebben we nu de vereiste dat de indexer altijd een veld bevat met de naam Id.
We willen deze vereiste verwijderen. Dit kan door in de indexer methode een delegate te gebruiken.

# Opgave 1:
-   Zorg dat je in de constructor van `AntaDataBuffer` een lambda kan meegeven. Het mag elk veld van het datatype `Guid` zijn.

-   Pas de indexer aan zodat de lambda gebruikt wordt.

-   Het is nu niet meer nodig dat de `PersonRow` van `AntaRow` wordt afgeleid. Refactor de code. Verplaats het veld `Id` van de `AntaRow` class naar de `PersonRow` class en verwijder de `AntaRow`. Zorg nu dat alles netjes is.

# Opgave 1 extra:
-   Als je tijd over hebt dan kun je ook de vereiste dat dat het sleutel veld een `Guid` veld is verwijderen. Voeg de volgende class toe.

```vbnet
Class CountryRow
  Property IsoCode as New AntaField(Of String)
  Property Name As New AntaField(Of String)
End Class
```

-   Zorg dat `AntaDataBuffer` kan werken met een generieke sleutel zodat zowel `CountryRow` met sleutel `IsoCode` werkt als de bestaande `PersonRow`.