using System.Globalization;
using Accommodations.Commands;
using Accommodations.Dto;

namespace Accommodations;

public static class AccommodationsProcessor
{
    private static BookingService _bookingService = new();
    private static Dictionary<int, ICommand> _executedCommands = new();
    private static int s_commandIndex = 0;

    public static void Run()
    {
        Console.WriteLine( "Booking Command Line Interface" );
        Console.WriteLine( "Commands:" );
        Console.WriteLine( "'book <UserId> <Category> <StartDate> <EndDate> <Currency>' - to book a room" );
        Console.WriteLine( "'cancel <BookingId>' - to cancel a booking" );
        Console.WriteLine( "'undo' - to undo the last command" );
        Console.WriteLine( "'find <BookingId>' - to find a booking by ID" );
        Console.WriteLine( "'search <StartDate> <EndDate> <CategoryName>' - to search bookings" );
        Console.WriteLine( "'exit' - to exit the application" );

        string input;
        while ( ( input = Console.ReadLine() ) != "exit" )
        {
            try
            {
                ProcessCommand( input );
            }
            catch ( ArgumentException ex )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
        }
    }

    private static void ProcessCommand( string input )
    {
        string[] parts = input.Split( ' ' );
        string commandName = parts[ 0 ];

        switch ( commandName )
        {
            case "book":
                if ( parts.Length != 6 )
                {
                    throw new ArgumentException( "Invalid number of arguments for booking." );
                }

                // FIX: Неправильный ввод айди пользователя теперь не завершает аварийно приложение 
                int userId;
                if ( !int.TryParse( parts[ 1 ], out userId ) )
                {
                    throw new ArgumentException( "UserId parsing error" );
                }

                // FIX: Неправильный ввод даты теперь не завершает аварийно приложение (далее - обработка парсинга)
                DateTime StartDate, EndDate;
                if ( !DateTime.TryParse( parts[ 3 ], out StartDate )
                    || !DateTime.TryParse( parts[ 4 ], out EndDate ) )
                {
                    throw new ArgumentException( "DateTime parsing error." );
                }

                // FIX: Кастомизировал ошибку неправильного ввода валюты
                CurrencyDto currency;
                if ( !Enum.TryParse( parts[ 5 ], true, out currency ) )
                {
                    throw new ArgumentException( $"{parts[ 5 ]} is not a valid currency." );
                }

                BookingDto bookingDto = new()
                {
                    UserId = userId,
                    Category = parts[ 2 ],
                    StartDate = StartDate,
                    EndDate = EndDate,
                    Currency = currency,
                };

                BookCommand bookCommand = new( _bookingService, bookingDto );
                bookCommand.Execute();
                _executedCommands.Add( ++s_commandIndex, bookCommand );
                Console.WriteLine( "Booking command run is successful." );
                break;

            case "cancel":
                if ( parts.Length != 2 )
                {
                    throw new ArgumentException( "Invalid number of arguments for canceling." );
                }

                // FIX: Обработка парсинга
                Guid bookingId;
                if ( !Guid.TryParse( parts[ 1 ], out bookingId ) )
                {
                    throw new ArgumentException( "BookingId parsing error." );
                }

                CancelBookingCommand cancelCommand = new( _bookingService, bookingId );
                cancelCommand.Execute();
                _executedCommands.Add( ++s_commandIndex, cancelCommand );
                Console.WriteLine( "Cancellation command run is successful." );
                break;

            case "undo":
                // FIX: undo без команд теперь не завершает аварийно приложение
                if ( _executedCommands.Count == 0 )
                {
                    throw new ArgumentException( "No commands to undo." );
                }

                _executedCommands[ s_commandIndex ].Undo();
                _executedCommands.Remove( s_commandIndex );
                s_commandIndex--;
                Console.WriteLine( "Last command undone." );

                break;
            case "find":
                if ( parts.Length != 2 )
                {
                    throw new ArgumentException( "Invalid arguments for 'find'. Expected format: 'find <BookingId>'" );
                }

                // FIX: Обработка парсинга
                Guid id;
                if ( !Guid.TryParse( parts[ 1 ], out id ) )
                {
                    throw new ArgumentException( "Id parsing error." );
                }

                FindBookingByIdCommand findCommand = new( _bookingService, id );
                findCommand.Execute();
                break;

            case "search":
                // FIX: Возможность поиска с опциональной категорией
                if ( parts.Length < 3 || parts.Length > 4 )
                {
                    throw new ArgumentException( "Invalid arguments for 'search'. Expected format: 'search <StartDate> <EndDate> [CategoryName]'" );
                }

                // FIX: Обработка парсинга
                DateTime startDate, endDate;
                if ( !DateTime.TryParse( parts[ 1 ], out startDate )
                    || !DateTime.TryParse( parts[ 2 ], out endDate ) )
                {
                    throw new ArgumentException( "DateTime parsing error." );
                }

                string categoryName = parts.Length > 3 ? parts[ 3 ] : "";
                SearchBookingsCommand searchCommand = new( _bookingService, startDate, endDate, categoryName );
                searchCommand.Execute();
                break;

            default:
                Console.WriteLine( "Unknown command." );
                break;
        }
    }
}
