We willen de data uit de xml file die we in de in de vorige oefening hebben ingelezen toevoegen aan sql. In Create.sql vind je het statement om de onderliggende tabel te creëren en te vullen met sample data.

# Opgave 1:
Pas het `PersonRow` type aan zodat deze geen properties van het type `AntaField<T>` meer bevat maar in plaats daarvan de .net basis types (o.a. `Guid` en `String`). We hebben `SqlValue` immers niet meer nodig.

Implementeer de `WriteToSql` functie in de runner en test of er daadwerkelijk 100 regels worden toegevoegd. Je mag zelf kiezen of je direct sql, dapper of efcore gebruikt.

# Opgave 2:
Alle 100 regels moeten in 1 transactie worden toegevoegd, implementeer dat. Zet een breakpoint als er 2 regels zijn toegevoegd en de transactie nog niet is afgerond. Kijk dan in sql (d.m.v. azure data studio of sql management studio) of deze regels inderdaad ontbreken als je een select doet op dat moment. Dat zou het geval moeten zijn.

# Opgave 3 extra:
Nu `PersonRow` geen speciale AntaFields meer bevat kunnen we het inlezen en wegschrijven naar xml of json ook via een normale serializer laten verlopen. Voeg een extension method toe op `AntaDataBuffer<T>` genaamd `ToJson` die een string retourneert.

Daarna kun je een extension method `LoadFromJson` toevoegen op het type `AntaDataBuffer<T>` waarmee je vanuit de eerder gemaakte json string een nieuwe databuffer kunt vullen d.m.v. de string uit `ToJson`.

Je kan je afvragen wat de meerwaarde nog is van `AntaDataBuffer` t.o.v. een normale .net dictionary met al deze extension methods 🤔.

# Opgave 4 extra:
Indien je in opgave 1 voor efcore hebt gekozen, maak dan ook een implementatie met dapper of omgekeerd. Maak ook een implementatie voor update en delete.
