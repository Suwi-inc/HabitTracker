using System.Data.SQLite;
using System.Reflection.PortableExecutable;

 string connectString = @"Data Source=mytestfile.sqlite;version = 3;";
//SQLiteConnection.CreateFile("mytestfile.sqlite");


SQLiteConnection _habitConnection = new SQLiteConnection(connectString);


CreateTable(_habitConnection);

Menu(_habitConnection);


static void Menu(SQLiteConnection _habitConnection)
{
    int choice = -1;
    Console.WriteLine("Please Selection Action ");
    while(choice != 0 )
    {
        Console.WriteLine("1 : Insert a Habit ");
        Console.WriteLine("2 : Remove a Habit ");
        Console.WriteLine("3 : Edit a Habit ");
        Console.WriteLine("4 : Show all Habits");
        Console.WriteLine("0 : Exit");
        
        choice = int.Parse(Console.ReadLine());
       
        switch(choice)
        {
            case 1: {
                    InsertHabit(_habitConnection);
                        }break;
            case 2: {
                    RemoveHabit(_habitConnection);
                        } break;
            case 3: { EditHabit(_habitConnection);
                } break;
            case 4: {
                    ListHabits(_habitConnection);
                        } break;
            case 0: break;
            default: break;
        }


    }
    
}
static void CreateTable( SQLiteConnection _habitConnection)
{
   
    _habitConnection.Open();

    string createCommandString = "Create table if not exists Habit (name varchar(50), quantity INT)";

    SQLiteCommand createCommand = new SQLiteCommand(createCommandString, _habitConnection);

    createCommand.ExecuteNonQuery();

}
static void InsertHabit(SQLiteConnection _habitConnection)
{
    int a = -1;
    while(a!= 0)
    {
        Console.WriteLine("1 : Insert a Habit ");
        Console.WriteLine("0 : Back ");
        a = int.Parse(Console.ReadLine());
        switch(a)
        {
            case 1: {
                    Console.WriteLine("Enter a Habit :");
                    string userhabit = Console.ReadLine();

                    Console.WriteLine("Enter the number of times you do it in a day :");

                    int habitquantity = int.Parse(Console.ReadLine());

                    string insertCommandString = $"Insert into Habit (name, quantity) values('{userhabit}','{habitquantity}')";

                    SQLiteCommand insertCommand = new SQLiteCommand(insertCommandString, _habitConnection);
                    insertCommand.ExecuteNonQuery();


                } break;
            case 0: break;
            default: break;
        }


    }

}
static void RemoveHabit(SQLiteConnection _habitConnection)
{
    int index;
    ListHabits(_habitConnection);
    Console.WriteLine("Enter the Index of the Habit you would like to remove : ");

    index = int.Parse(Console.ReadLine());

    string removeCommandString = $"DELETE FROM Habit WHERE rowid = {index}";

    SQLiteCommand removeCommand = new SQLiteCommand(removeCommandString, _habitConnection);
    removeCommand.ExecuteNonQuery();

    ListHabits(_habitConnection);


}
static void EditHabit(SQLiteConnection _habitConnection)
{
    int index;
    ListHabits(_habitConnection);
    Console.WriteLine("Enter the Index of the Habit you would like to change : ");
    index = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter a new Habit name:");
    string userhabit = Console.ReadLine();

    Console.WriteLine("Enter the number of times you do it in a day :");

    int habitquantity = int.Parse(Console.ReadLine());

    string alterCommandString = $"UPDATE Habit SET name = '{userhabit}' , quantity = '{habitquantity}' WHERE rowid = {index}";

    SQLiteCommand alterCommand = new SQLiteCommand(alterCommandString, _habitConnection);
    alterCommand.ExecuteNonQuery();

    ListHabits(_habitConnection);


}
static void ListHabits(SQLiteConnection _habitConnection)
{
    int counter = 0;
    string selectCommandString = "Select * from Habit";

    SQLiteCommand selectCommand = new SQLiteCommand(selectCommandString, _habitConnection);

    SQLiteDataReader values = selectCommand.ExecuteReader();
    while (values.Read())
        Console.WriteLine(++counter +"Name: " + values["name"] + "\tAmount: " + values["quantity"]);

}


