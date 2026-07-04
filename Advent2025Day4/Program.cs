using Advent2025Day4;

//var inputFile = @"C:\Dev\Advent Of Code\Advent2025\Advent2025Day4\input.txt";
////var numCanMove = PartOne.GetNumberOfRollsThatCanBeMoved(inputFile);
//var numCanMove = PartTwo.GetTotalRollsThatCanBeMoved(inputFile);

//Console.WriteLine($"Number of rolls that can be moved: {numCanMove}");

var areas = new List<string[]> {
    new[] {"VillageA", "State1"},
    new[] {"VillageB", "State1"},
    new[] {"VillageC", "State1"},
    new[] {"VillageD", "State2"},
    new[] {"VillageE", "State2"},
    new[] {"VillageF", "State3"},
    new[] {"State1", "Country1"},
    new[] {"State2", "Country1"},
    new[] {"State3", "Country2"},

};

var person = new Person
{
    Name = "Sam",
    Area = "VillageF"
};

bool IsPersonInArea(Person person, string area)
{
    if (person.Area == area)
        return true;

    var subAreas = areas.Where(a => a[1] == area).Select(s => s[0]);

    foreach (var subArea in subAreas)
    {
        if (IsPersonInArea(person, subArea))
            return true;
        else
            continue;
    }

    return false;
}

var area = "Country1";

var status = IsPersonInArea(person, area) ? "is" : "is not";

Console.WriteLine($"{person.Name} {status} in {area}");


class Person
{
    public string Name { get; set; }
    public string Area { get; set; }
}

