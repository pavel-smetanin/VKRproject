using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using VKRproject.Models;
using VKRproject.Tools;


namespace VKRproject.Modules
{
    public class TelegramModule
    {
        private static string Token  = ConfigProvider.PrivateConfig["Telegram:BotToken"];
        public static string BotUrl { get; private set; } = ConfigProvider.PrivateConfig["Telegram:BotUrl"];
        private TelegramBotClient Client { get; set; }
        public QuestionFilter Filter { get; private set; }
        public Dictionary<int, string> Countries { get; set; }
        public int Code { get; private set; }
        private bool UserCheck;
        private int DialogStatus;
        public TelegramModule() 
        {
            Client = new TelegramBotClient(Token);
            Filter = new QuestionFilter();
            UserCheck = false;
            DialogStatus = 0;
            Code = GenerateCode();
            Countries = ModelTool.GetCountriesDict();
            
        }
        public void StartBot()
        {
            Client.StartReceiving();
            Client.OnMessage += Client_OnMessage;
            Client.OnCallbackQuery += Client_OnCallbackQuery;
            Console.WriteLine("Запущен бот " + Client.GetMeAsync().Result.FirstName);
            Console.WriteLine("Код: " + Code);
            
        }
        public void FinishBot()
        {
            if(Client.IsReceiving)
                Client.StopReceiving();
            return;
        }

        private async void Client_OnCallbackQuery(object? sender, CallbackQueryEventArgs e)
        {
            if (e.CallbackQuery.Data != null)
            {
                if (UserCheck == true && DialogStatus == 1)
                {
                    Console.WriteLine(e.CallbackQuery.Data.ToString());
                    Filter.CountryID = Int32.Parse(e.CallbackQuery.Data.ToString());
                    Console.WriteLine(Filter.CountryID);
                    await Client.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, $"Вы выбрали {e.CallbackQuery.Message.Text}");
                    await Client.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Введите желаемые даты периодов тура. Вводите даты в формате чч.мм.гггг"); ; ;
                    await Client.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Период вылета. Введите дату начала периода вылета:");
                    DialogStatus = 2;
                    return;
                }
            }
            return;
        }

        private async void Client_OnMessage(object? sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                if (e.Message.Text.StartsWith("/")) //Обработка команд
                {
                    if (e.Message.Text == "/start")
                    {
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Добро пожаловать в бот анкеты!");
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Наш менеджер сообщит вам уникальный код. Введите полученный код для начала анкеты");
                        return;
                    }
                    if (e.Message.Text == "/info")
                    {
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "!!!ИНСТРУКЦИЯ ПО РАБОТЕ С БОТОМ!!!\n" +
                            "Для начала анкетирования вам необходимо попросить уникальный код у менеджера." +
                            "Код является одноразовым и дает возможность заполнять только оду анкету." +
                            "После того как вы ввели код вам предлагается ответить на небольшой круг вопросов." +
                            "На основе ваших ответов будут подобраны критерии для поиска и подбора туров." +
                            "После завершения анкеты менеджер увидит вашу подборку и проконсультирует по ней, " +
                            "при необходимости он отправит вам подборку сюда");
                        return;
                    }
                    if (e.Message.Text == "/help")
                    {
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "!!!ПОМОЩЬ ПО КОМАНДАМ!!!\n" +
                            "/start - начало работы бота. Если вы уже запустили бота повторное использование команды не требуется\n" +
                            "/help - помощь командам\n" +
                            "/info - инструкция по использованию бота\n" +
                            "/cancel - отмена введенного ответа. ВНИМАНИЕ! Предыдущий ответ сбрасывается" +
                            "/reset - сброс введенных ответов в анкете. ВНИМАНИЕ! Все ответы сбрасываются, ввод анкеты осуществляется повторно\n" +
                            "/delete - удаление текущей сессии анкеты. ВНИМАНИЕ! Сессия пользователя бота завершается\n" +
                            "/finish - завершение анкеты и отправка ответов\n");
                        return;
                    }
                    if (e.Message.Text == "/cancel")
                    {
                        if (DialogStatus > 0)
                        {
                            await Client.SendTextMessageAsync(e.Message.Chat.Id, "Отмена предыдущего ответа!");
                            DialogStatus--;
                            return;
                        }
                        else
                        {
                            await Client.SendTextMessageAsync(e.Message.Chat.Id, "Отменить предыдущее действие невозможно!");
                        }
                    }
                    if(e.Message.Text == "/reset")
                    {
                        Filter.Reset();
                        DialogStatus = 0;
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Вы сбросили анкету! Ввод анкеты теперь осуществляется заново");
  
                    }
                }
                if (e.Message.Text == Code.ToString() && UserCheck == false) //Авторизация
                {
                    UserCheck = true;
                    await Client.SendTextMessageAsync(e.Message.Chat.Id, "Проверка пройдена! Помощь по командам: /help");
                    await Client.SendTextMessageAsync(e.Message.Chat.Id,"Выберите страну", replyMarkup: GetKeyboard());
                    DialogStatus = 1;
                    return;
                }
                
                if (UserCheck == true) //Диалог
                {
                    
                    if (DialogStatus == 1)
                    {
                        //Filter.CountryName = e.Message.Text;
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Введите желаемые даты периодов тура. Вводите даты в формате чч.мм.гггг");
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Период вылета. Введите дату начала периода вылета:");
                        DialogStatus = 2;
                        return;
                    }
                    if (DialogStatus == 2)
                    {
                        Filter.StartDateLower = e.Message.Text;
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Период вылета. Введите дату окончания периода вылета:");
                        DialogStatus = 3;
                        return;
                    }
                    if (DialogStatus == 3)
                    {
                        Filter.StartDateUpper = e.Message.Text;
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Введите желаемую дату вылета обратно: ");
                        DialogStatus = 4;
                        return;
                    }
                    if (DialogStatus == 4)
                    {
                        Filter.EndDate = e.Message.Text;
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Введите желаемое количество ночей: ");
                        DialogStatus = 5;
                        return;
                    }
                    if (DialogStatus == 5)
                    {
                        Filter.NightsCount = Int32.Parse(e.Message.Text);
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Введите количество туристов");
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Количество взрослых: ");
                        DialogStatus = 6;
                        return;
                    }
                    if (DialogStatus == 6)
                    {
                        Filter.AdultsCount = Int32.Parse(e.Message.Text);
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Количество детей: ");
                        DialogStatus = 7;
                        return;
                    }
                    if (DialogStatus == 7)
                    {
                        Filter.ChildCount = Int32.Parse(e.Message.Text);
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Введите желаемую стоимость тура в рублях");
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Стоимость от:");
                        DialogStatus = 8;
                        return;
                    }
                    if (DialogStatus == 8)
                    {
                        Filter.PriceLower = Int32.Parse(e.Message.Text);
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Стоимость до:");
                        DialogStatus = 9;
                        return;
                    }
                    if (DialogStatus == 9)
                    {
                        Filter.PriceUpper = Int32.Parse(e.Message.Text);
                        await Client.SendTextMessageAsync(e.Message.Chat.Id, "Данные анкеты введены. Проверка...");
                        if (Filter.IsFieldFill())
                        {
                            await Client.SendTextMessageAsync(e.Message.Chat.Id, "Анкета успешно пройдена. Далее вас проконсультирует менеджер");
                            DialogStatus = 0;
                            UserCheck = false;
                            Console.WriteLine(Filter.CountryID + ", " + Filter.PriceUpper);
                            return;
                        }
                        return;
                    }
                }
                else
                {
                    await Client.SendTextMessageAsync(e.Message.Chat.Id, "Для прохождения анкеты требуется код! Наш менеджер должен вам его сообщить");
                    return;
                }
                return;
            }
        }
        private InlineKeyboardMarkup GetKeyboard()
        {
            var buttons = new List<InlineKeyboardButton[]>();
            foreach(var c in Countries)
            {
                buttons.Add(new[] { InlineKeyboardButton.WithCallbackData(c.Value, c.Key.ToString()) });
            }
            var kbMurkup = new InlineKeyboardMarkup(buttons);
           
            return kbMurkup;
        }

        public int GenerateCode()
        {
            Random rnd = new Random();
            int code = rnd.Next(100000, 999999);
            return code;
        }
    }
}