In MainWindow schrijven we een bestand. Als we een trage verbinding hebben zou deze actie potentieel lang kunnen duren. In de huidige implementatie bevriest de UI.

# Opdracht 1:
Bekijk de code van het programma. Voer het uit en druk op de start knop. Probeer direct na het drukken op start het formulier te verplaatsen.

# Opdracht 2:
Zorg dat de UI niet meer bevriest door het schrijven van het bestand op de threadpool uit te voeren. Implementeer dit in de `File` class.

# Opdracht 3:
Zorg dat de File class een event geeft wanneer het schrijven voltooid is. In de UI kun je "De file is geschreven" op het scherm tonen wanneer dit event komt. Let op, we introduceren nu state in de static `File` class. Zorg dat de `File` class niet geen static state bevat is zodat je geen rekening hoeft te houden met threading.

# Opdracht 4:
Het kan natuurlijk zijn dat het schrijven van de file fout gaat. Zet om dit te simuleren `throw new Exception("demo");` in de File.Write methode. Kijk hoe .NET standaard met deze error om gaat. Zorg nu dat de error netjes d.m.v. een event aan de UI wordt doorgegeven d.m.v. de event.

# Opdracht 4 extra:
Haal de `throw new exception("demo");` weg, maar schijf de code zo dat als er gedurende de CreateDirectory of WriteAllText een exception zou optreden er netjes een fout in de event komt.