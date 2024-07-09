string productName = ReadString( "Введите название товара: " );
uint productQuantity = ReadQuantity();
string userName = ReadString( "Введите ваше имя: " );
string address = ReadString( "Введите ваш адрес: " );

Console.WriteLine();
ConfirmOrder( productName, productQuantity, userName, address );

string ReadString( string hint = "", bool required = true )
{
    if ( !String.IsNullOrWhiteSpace( hint ) )
    {
        Console.Write( hint );
    }

    string input = Console.ReadLine();
    while ( required && String.IsNullOrWhiteSpace( input ) )
    {
        Console.Write( "Строка не может быть пустой, попробуйте ещё раз: " );
        input = Console.ReadLine();
    }

    return input;
}

uint ReadQuantity()
{
    Console.Write( "Введите количество товара: " );

    uint quantity;
    while ( !uint.TryParse( Console.ReadLine(), out quantity ) || quantity == 0 )
    {
        Console.Write( "Ошибка чтения - введите число (больше 0): " );
    }

    return quantity;
}

void ConfirmOrder( string productName, uint productQuantity, string userName, string address )
{
    string answer = ReadString( $"Здравствуйте, {userName}, вы заказали {productQuantity} {productName} на адрес {address}, все верно? (да/нет): " );

    while ( true )
    {
        if ( answer.ToLower() == "да" )
        {
            string deliveryDate = DateTime.Today.AddDays( 3 ).ToString( "d" );
            Console.WriteLine( $"{userName}! Ваш заказ {productName} в количестве {productQuantity} оформлен! Ожидайте доставку по адресу {address} к {deliveryDate}" );
        }
        else if ( answer.ToLower() == "нет" )
        {
            Console.WriteLine( "Оформление заказа было отменено." );
        }
        else
        {
            answer = ReadString( "Ошибка чтения, вам необходимо ввести 'да' или 'нет': " );
            continue;
        }

        break;
    }
}