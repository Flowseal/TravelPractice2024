const string FileName = "dict.txt";
var dictionary = new Dictionary<string, string>();
DictionaryLoop();

void PrintMainMenu()
{
    Console.Write( """

        Выберите пункт меню:
        1. Начать переводить
        2. Добавить/изменить/удалить перевод
        3. Выход
        > 
        """ );
}

void PrintEditMenu()
{
    Console.Write( """
        1. Изменить
        2. Удалить
        3. Назад
        > 
        """ );
}

int ReadMenuChoice()
{
    int choice;
    while ( !int.TryParse( Console.ReadLine(), out choice ) )
    {
        Console.Write( "Неверный выбор, попробуйте снова > " );
    }

    return choice;
}

void SaveDictionary()
{
    using ( StreamWriter stream = new StreamWriter( FileName ) )
    {
        foreach ( KeyValuePair<string, string> entry in dictionary )
        {
            stream.WriteLine( $"{entry.Key}|{entry.Value}" );
        }
    }
}

void LoadDictionary()
{
    if ( !File.Exists( FileName ) )
    {
        using ( File.Create( FileName ) ) { }
        return;
    }

    dictionary.Clear();
    using ( StreamReader stream = new StreamReader( FileName ) )
    {
        string line;
        while ( ( line = stream.ReadLine() ) != null )
        {
            if ( !line.Contains( "|" ) )
                continue;

            dictionary.Add( line.Split( "|" )[ 0 ], line.Split( "|" )[ 1 ] );
        }
    }
}

bool EditDictionaryTranslation( string original )
{
    Console.Write( "Перевод: " );
    string translate = Console.ReadLine().Trim().ToLower();

    if ( String.IsNullOrWhiteSpace( translate ) )
    {
        Console.WriteLine( "Перевод не сохранён." );
        return false;
    }

    if ( dictionary.ContainsKey( original ) == dictionary.ContainsKey( translate )
                    || dictionary.ContainsKey( original ) )
    {
        // Новый перевод или изменение
        dictionary[ original ] = translate;
        dictionary[ translate ] = original;
    }
    else if ( dictionary.ContainsKey( translate ) )
    {
        // Новая вариация перевода
        dictionary[ original ] = translate;
    }

    return true;
}

void StartWordsTranslating()
{
    Console.WriteLine( "\nВведите слово для перевода" );
    Console.WriteLine( "Для выхода введите пустую строку" );

    while ( true )
    {
        Console.Write( "\nСлово: " );
        string original = Console.ReadLine().Trim().ToLower();

        if ( String.IsNullOrWhiteSpace( original ) )
        {
            break;
        }

        if ( !dictionary.ContainsKey( original ) )
        {
            Console.WriteLine( "Перевод не найден; добавьте перевод или пропустите, введя пустую строку." );
            if ( EditDictionaryTranslation( original ) )
            {
                SaveDictionary();
            }
        }
        else
        {
            Console.WriteLine( $"Перевод: {dictionary[ original ]}" );
        }
    }
}

void AddOrEditWords()
{
    Console.WriteLine( "\nВведите слово для добавления или изменения" );
    Console.WriteLine( "Для выхода введите пустую строку" );

    while ( true )
    {
        Console.Write( "\nСлово: " );
        string original = Console.ReadLine().Trim().ToLower();

        if ( String.IsNullOrWhiteSpace( original ) )
        {
            break;
        }

        if ( dictionary.ContainsKey( original ) )
        {
            PrintEditMenu();

            switch ( ReadMenuChoice() )
            {
                case 1: // Изменить
                    {
                        EditDictionaryTranslation( original );
                        break;
                    }
                case 2: // Удалить
                    {
                        dictionary.Remove( original );
                        Console.WriteLine( $"{original} удалено!" );
                        break;
                    }
                case 3:
                default: // Назад
                    {
                        continue;
                    }
            }
        }
        else
        {
            EditDictionaryTranslation( original );
        }
    }
}

void DictionaryLoop()
{
    Console.WriteLine( "Добро пожаловать в приложение-словарь! " );
    LoadDictionary();

    while ( true )
    {
        PrintMainMenu();

        switch ( ReadMenuChoice() )
        {
            case 1:
                {
                    StartWordsTranslating();
                    break;
                }
            case 2:
                {
                    AddOrEditWords();
                    SaveDictionary();
                    break;
                }
            case 3:
                {
                    Console.WriteLine( "До встречи!" );
                    return;
                }
            default:
                {
                    Console.WriteLine( "Неизвестная команда!" );
                    break;
                }
        }
    }
}