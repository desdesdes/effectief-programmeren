namespace Exercises.Exercise2;

class Runner
{
  public static void RunMe()
  {
    var persons = new AntaDataBuffer<PersonRow>();

    //// Commentaar opgave 1
    //var personRow = new PersonRow();
    //personRow.Id.Value = Guid.NewGuid();
    //personRow.FirstName.Value = "Larry";
    //personRow.LastName.Value = "Page";
    //persons.Add(personRow);

    //var myGuid = Guid.NewGuid();

    //personRow = new PersonRow();
    //personRow.Id.Value = myGuid;
    //personRow.FirstName.Value = "Sergey";
    //personRow.LastName.Value = "Brin";
    //persons.Add(personRow);

    //Console.WriteLine("List:");

    //foreach (var person in persons)
    //{
    //  Console.WriteLine($"{person.FirstName.Value} {person.LastName.Value}");
    //}

    //// Commentaar opgave 2
    //Console.WriteLine();
    //Console.WriteLine("Indexer:");
    //Console.WriteLine($"{persons[myGuid].FirstName.Value} {persons[myGuid].LastName.Value}");
  }
}
