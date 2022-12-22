using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Extensions;
using System.Threading;
using Bot;

namespace Бот
{
    class Program
    {
        /*Планы:
         * 1. !!! Сделать лимит на 30 сообщений в секунду
         * 2. Подписка
         * 3. WEB 2.0
        */

        static string token = "5226620701:AAEyj3dA2hwE4PeNGAu8K-qHtSq335gLIE0";
        static ITelegramBotClient bot = new TelegramBotClient(token);
        static List<Registration> NewRegistr = new List<Registration>();
        static List<CreatingSection> creatingSections = new List<CreatingSection>();
        static List<AnketsSend> Viewed = new List<AnketsSend>();
        static List<Reports> Reporting = new List<Reports>();
        static List<MessagesStack> MS = new List<MessagesStack>();
        static List<AnonimChat> AnonimChats = new List<AnonimChat>();
        static List<SelectSection> SelectSections = new List<SelectSection>();
        static List<SectionParams> newSectionParams = new List<SectionParams>();

        static List<UsersKarma> UK = new List<UsersKarma>();
        static List<LikedUsers> LU = new List<LikedUsers>();
        static List<LastMessage> LastMessages = new List<LastMessage>();
        static ReplyKeyboardRemove rr = new ReplyKeyboardRemove();
        static Random rnd = new Random();

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                Users currentUser = await ModelU.GetContext().Users.FindAsync(message.From.Id);
                if(currentUser != null && currentUser.Active == false && currentUser.photo != null)
                {
                    currentUser.Active = true;
                    await ModelU.GetContext().SaveChangesAsync();
				}
                LastMessage lm = LastMessages.Find(x => x.userid == message.From.Id);
                if (lm != null && lm.date == (long)DateTime.UtcNow.Subtract(new DateTime(1970,1,1)).TotalSeconds)
                {
                    return;
                }
                else if (lm != null)
                {
                    lm.date = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
				}
                else
                {
                    LastMessage newMessage = new LastMessage()
                    {
                        date = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                        userid = message.From.Id
					};
                    LastMessages.Add(newMessage);
				}
                ReplyKeyboardMarkup keyboard = Keyboards.showMainKeyboard();
                if (message.Text != null)
                {
                    if(message.Text.ToLower() == "/start")
                    {
                        if (currentUser == null)
                        {
                            Users u = new Users();
                            if (message.From.Username == null)
                            {
                                u.username = message.From.Id.ToString();
                            }
                            else u.username = message.From.Username;
                            u.id = message.From.Id;
                            u.Active = false;
                            u.VIP = false;
                            u.Karma = 0;
                            u.Rating = 0;
                            u.LikedCount = 0;
                            u.ViewCount = 0;
                            ModelU.GetContext().Users.Add(u);
                            ModelU.GetContext().SaveChanges();
                            await botClient.SendTextMessageAsync(message.Chat, "Привет, ты зарегистрирован🖐🏻\nДля корректной работы рекомендуем установить в настройках ваш @username\n" +
							"Обо всех багах прошу сообщать мне - @Vimer7 \nПока что не работает функция подбора анкет по полу и городу, т.к очень мало анкет", ParseMode.Html, null, false, false, 0, true, keyboard);
                            return;
                        }
                        await botClient.SendTextMessageAsync(message.Chat, "Вы уже зарегистрированы⚠️", ParseMode.Html, null, false, false, 0, true, keyboard);
                        return;
                    }
                    if(currentUser == null)
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Пропишите /start");    
                        return;
					}

                    if (message.Text == "👥Анонимный чат")
                    {
                        generateNewChat(botClient, message.From.Id, "");
                        return;
                    }
                    AnonimChat activeChat = AnonimChats.Find(x => x.user1 == message.From.Id || x.user2 == message.From.Id);
                    if (message.Text == "👤Следующий")
                    {
                        if (activeChat == null) return;
                        if (activeChat.user1 == message.From.Id)
                        {
                            if (activeChat.user2 == 0)
                            {
                                await botClient.SendTextMessageAsync(message.Chat, "У вас и так нет собеседника");
                                return;
                            }
                            generateNewChat(botClient, activeChat.user2, "Собеседник вышел из чата.\n");
                            AnonimChats.Remove(activeChat);
                            generateNewChat(botClient, message.From.Id, "");
                            return;
                        }
                        if (activeChat.user2 == message.From.Id)
                        {
                            if (activeChat.user2 == 0)
                            {
                                await botClient.SendTextMessageAsync(message.Chat, "У вас и так нет собеседника");
                                return;
                            }
                            generateNewChat(botClient, activeChat.user1, "Собеседник вышел из чата.\n");
                            AnonimChats.Remove(activeChat);
                            generateNewChat(botClient, message.From.Id, "");
                            return;
                        }
                    }
                    if (message.Text == "🛑Стоп")
                    {
                        if (activeChat == null)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Меню:", ParseMode.Html, null, false, false, 0, true, Keyboards.showMainKeyboard());
                            return;
                        }
                        await botClient.SendTextMessageAsync(message.Chat, "Меню:", ParseMode.Html, null, false, false, 0, true, Keyboards.showMainKeyboard());
                        long user1 = activeChat.user1, user2 = activeChat.user2;
                        if (activeChat == null) return;
                        AnonimChats.Remove(activeChat);
                        if (user1 == message.From.Id)
                        {
                            if (user2 != 0)
                                generateNewChat(botClient, user2, "Собеседник вышел из чата.\n");
                        }
                        if (user2 == message.From.Id)
                        {
                            if (user1 != 0)
                                generateNewChat(botClient, user1, "Собеседник вышел из чата.\n");
                        }
                        
                        return;
                    }
                    if (message.Text == "⚠️Жалоба")
                    {
                        if(activeChat == null) return;
                        if (activeChat.user1 == 0 || activeChat.user2 == 0)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Еще нет собеседника");
                            return;
                        }
                        long target = 0;
                        if (activeChat.user1 == message.From.Id)
                        {
                            target = activeChat.user2;
                        }
                        else
                        {
                            target = activeChat.user1;
                        }
                        Reports rep = new Reports()
                        {
                            Sender = message.From.Id,
                            Target = target
                        };
                        Reporting.Add(rep);
                        await botClient.SendTextMessageAsync(message.Chat, "Укажите нарушение:", ParseMode.Html, null, false, false, 0, true, Keyboards.showCancelB());
                        return;
                    }
                    if (message.Text == "❎Отмена")
                    {
                        if (AnonimChats.Find(x => x.user1 == message.From.Id || x.user2 == message.From.Id) != null)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Продолжай общение", null, null, null, null, null, null, Keyboards.showAnonimChatKeyboard());
                            Reporting.Remove(Reporting.Find(x => x.Sender == message.From.Id));
                            return;
                        }
                        sendNewAnket(botClient, message, currentUser);
                        return;
                    }
                    if (Reporting.Find(x => x.Sender == message.From.Id) != null && AnonimChats.Find(x => x.user1 == message.From.Id || x.user2 == message.From.Id) != null)
                    {
                        if (activeChat.user1 == 0 || activeChat.user2 == 0)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Еще нет собеседника");
                            return;
                        }
                        var myReport = Reporting.Find(x => x.Sender == message.From.Id);
                        myReport.Reason = message.Text;
                        ModelU.GetContext().Reports.Add(myReport);
                        ModelU.GetContext().SaveChanges();
                        Reporting.Remove(myReport);
                        await botClient.SendTextMessageAsync(message.Chat, "Спасибо за помощь, мы обязательно проверим жалобу!👍", null, null, null, null, null, null, Keyboards.showAnonimChatKeyboard());

                        return;
                    }
                    if (activeChat != null)
                    {
                        if (activeChat.user1 == 0 || activeChat.user2 == 0)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Собеседник пока не найден😔");
                            return;
                        }
                        if (activeChat.user1 == message.From.Id)
                        {
                            await botClient.SendTextMessageAsync(activeChat.user2, message.Text);
                            return;
                        }
                        if (activeChat.user2 == message.From.Id)
                        {
                            await botClient.SendTextMessageAsync(activeChat.user1, message.Text);
                            return;
                        }
                    }
                    
                    if (message.Text == "⚙Информация")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "👨🏻‍💻 Тех.поддержка - @Vimer7 \nСтатус - альфа-тест \nДата основания: 07.04.2022\n\n" +
                        "✨Как получать карму?\n1. Оценка анкеты (+0.01)\n2. Достоверная жалоба(+0.5)\n\n" +
                        "📊Как считается рейтинг?\n Рейтинг = оценили анкету / количество просмотров * 10\nДанные обновляются раз в минуту");
                        return;
                    }
                    if(message.Text == "👤Мой профиль")
                    {
                        sendProfile(botClient, currentUser, message.Chat);
                        return;
				    }
                    if (message.Text == "🔌Отключить анкету")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Ваша анкета отключена, до скорой встречи🛏", ParseMode.Html, null, false, false, 0, true, Keyboards.showMainKeyboard());
                        currentUser.Active = false;
                        ModelU.GetContext().SaveChanges();
                        return;
                    }

                    if (message.Text == "♥️Оценивать")
                    {   
                        if(ModelU.GetContext().Users.Find(message.From.Id).photo == null)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Сначала заполните профиль", ParseMode.Html, null, false, false, 0, true, Keyboards.showMainKeyboard());
                            return;
                        }
                        sendNewAnket(botClient, message, currentUser);
                        return;
					}
                    if(message.Text == "♥️")
                    {
                        var unMarkedAnk = MS.Where(x => x.getter == message.From.Id && x.type == 1).ToList();
                        //надо работать с unMarkedAnk, слеш в фото должен быть / а не \ 
                        if (unMarkedAnk.Count != 0)
                        {
                            var sender = Viewed.FindLast(v => v.user1 == message.From.Id).user2;
                            var curMS = unMarkedAnk.Find(x => x.getter == message.From.Id && x.sender == sender);
                            MS.Remove(curMS);
                            await botClient.SendTextMessageAsync(sender.ToString(), "Получена взаимная симпатия!😍\nИмя - [" + currentUser.FirstName + "](tg://user?id=" + currentUser.id + ")", ParseMode.Markdown);//\nСсылка - @" + message.From.Username, ParseMode.Markdown);
                            var sendFM = ModelU.GetContext().Users.Find(sender);
                            await botClient.SendTextMessageAsync(message.Chat, "Обязательно напиши понравившемуся человеку!😍\nИмя - [" + sendFM.FirstName + "](tg://user?id=" + sendFM.id + ")", ParseMode.Markdown);//\nСсылка - @" + sendFM.username, ParseMode.Markdown);
                            editLike(sender);
                            editKarma(message.From.Id, 0.01);
                            sendNewAnket(botClient, message, currentUser);
                            return;
                        }
                        var getter = Viewed.FindLast(x => x.user1 == message.From.Id);
                        if (getter == null)
                        {
                            return;
                        }
                        MessagesStack LikeMess = new MessagesStack();
                        LikeMess.sender = message.From.Id;
                        LikeMess.getter = getter.user2;
                        LikeMess.type = 1;
                        editLike(getter.user2);
                        MS.Add(LikeMess);
                        editKarma(message.From.Id, 0.01);
                        sendNewAnket(botClient, message, currentUser);
                        return;
                    }
                    if(message.Text == "👎")
                    {
                        var unMarkedAnk = MS.Where(x => x.getter == message.From.Id && x.type == 1).ToList();
                        if (unMarkedAnk.Count != 0)
                        {
                            var sender = Viewed.FindLast(v => v.user1 == message.From.Id).user2;
                            var curMS = MS.Find(x => x.getter == message.From.Id && x.sender == sender);
                            MS.Remove(curMS);
                        }
                        editKarma(message.From.Id, 0.01);
                        sendNewAnket(botClient, message, currentUser);
                        return;
                    }
                    if(message.Text == "🛑")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Меню:", ParseMode.Html, null, false, false, 0, true, Keyboards.showMainKeyboard());
                        return;
                    }
                    if(message.Text == "⚠️" && Viewed.FindLast(x => x.user1 == message.From.Id) != null)
                    {
                        Reports rep = new Reports()
                        {
                            Sender = message.From.Id,
                            Target = Viewed.FindLast(x => x.user1 == message.From.Id).user2
                        };
                        Reporting.Add(rep);
                        await botClient.SendTextMessageAsync(message.Chat, "Укажите нарушение:", ParseMode.Html, null, false, false, 0, true, Keyboards.showCancelB());
                        return;
                    }
                    if(Reporting.Find(x => x.Sender == message.From.Id) != null)
                    {
                        var myReport = Reporting.Find(x => x.Sender == message.From.Id);
                        myReport.Reason = message.Text;
                        ModelU.GetContext().Reports.Add(myReport);
                        ModelU.GetContext().SaveChanges();
                        Reporting.Remove(myReport);
                        await botClient.SendTextMessageAsync(message.Chat, "Спасибо за помощь, мы обязательно проверим жалобу!👍");
                        sendNewAnket(botClient, message, currentUser);
                        return;
                    }

                    if(message.Text == "🎆WEB 2.0")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "WEB 2.0🎆", ParseMode.Html, null, false, false, 0, true, Keyboards.showMainWebKeyboard());
                        return;
					}
                    if(message.Text == "📄Разделы")
                    {
                        List<Section> sections = ModelU.GetContext().Section.Where(x => x.Popularity >= 0).ToList();
                        if(sections.Count == 0)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Разделов пока что нет😔");
                            return;
                        }
                        string sectionString = "Список разделов(напиши номер раздела):\n";
                        foreach(Section section in sections)
                        {
                            sectionString += "" + section.id + ". " + section.Name + "\n";
                        }
                        await botClient.SendTextMessageAsync(message.Chat, sectionString);

                        SelectSection ss = new SelectSection();
                        ss.id = currentUser.id;
                        ss.type = 0;
                        SelectSections.Add(ss);

                        return;
                    }
                    if (message.Text == "📑Мои разделы")
                    {
                        List<Section> sections = ModelU.GetContext().Section.Where(x => x.Creator == currentUser.id).ToList();
                        if (sections.Count == 0)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Разделов пока что нет😔", null, null, null, null, null, null, Keyboards.showMyWebKeyboard());
                            return;
                        }
                        string sectionString = "Список твоих разделов(напиши номер раздела):\n";
                        foreach (Section section in sections)
                        {
                            sectionString += "" + section.id + ". " + section.Name + "\n";
                        }
                        await botClient.SendTextMessageAsync(message.Chat, sectionString, null, null, null, null, null, null, Keyboards.showMyWebKeyboard());

                        SelectSection ss = new SelectSection();
                        ss.id = currentUser.id;
                        ss.type = 1;
                        SelectSections.Add(ss);

                        return;
                    }
                    if (message.Text == "Создать раздел✅")
                    {
	                    CreatingSection cs = new CreatingSection();
	                    cs.id = currentUser.id;
	                    cs.stage = 0;
                        creatingSections.Add(cs);
                        await botClient.SendTextMessageAsync(message.Chat, "Введите название раздела");
                        return;
                    }
                    if (message.Text == "Создать анкету✅")
                    {

						return;
                    }
                    CreatingSection csSection = creatingSections.Find(x => x.id == currentUser.id);
                    if(message.Text == "Готово✅")
                    {
	                    var SectPars = newSectionParams.Where(x => x.SectionID == csSection.sectionId).ToList();
	                    if (SectPars.Count == 0)
	                    {
		                    await botClient.SendTextMessageAsync(message.Chat, "Вы не указали параметры⚠️");
                            return;
	                    }
	                    foreach (var x in SectPars)
	                    {
		                    ModelU.GetContext().SectionParams.Add(x);
		                    newSectionParams.Remove(x);
	                    }
	                    ModelU.GetContext().SaveChanges();
	                    creatingSections.Remove(csSection);
	                    await botClient.SendTextMessageAsync(message.Chat, "Раздел сохранен✅", null, null, null, null, null, null,Keyboards.showMainWebKeyboard());
                        return;
                    }
                    if (message.Text == "Отмена❌")
                    {
	                    creatingSections.Remove(csSection);
	                    await botClient.SendTextMessageAsync(message.Chat, "Создание отменено", null, null, null, null, null, null, Keyboards.showMainWebKeyboard());
                        return;
                    }
                    if (csSection != null)
                    {
	                    csSection.stage++;
	                    if (csSection.stage == 1)
	                    {
		                    Section newSection = new Section();
		                    newSection.Name = message.Text;
		                    newSection.Popularity = 0f;
		                    newSection.Creator = currentUser.id;
		                    ModelU.GetContext().Section.Add(newSection);
		                    ModelU.GetContext().SaveChanges();
		                    csSection.sectionId = newSection.id;
		                    await botClient.SendTextMessageAsync(message.Chat, "Готово, теперь укажи параметры параметры отдельными сообщениями:",null, null, null, null, null, null, Keyboards.showCreatingSectionKeyboard());
                            return;
	                    }
	                    SectionParams sp = new SectionParams();
	                    sp.SectionID = csSection.sectionId;
	                    sp.ParamName = message.Text;
                        newSectionParams.Add(sp);
                        await botClient.SendTextMessageAsync(message.Chat, "Количество параметров: " + (csSection.stage - 1).ToString());
                        return;
                    }

                    if (message.Text == "🛒О подписке")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Еще успеешь задонатить", ParseMode.Html, null, false, false, 0, true, Keyboards.showMainKeyboard());
                        return;
                    }
                    if(message.Text == "🏆Лидеры")
                    {
                        List<Users> users = ModelU.GetContext().Users.ToList();
                        users.OrderByDescending(x => x.Rating);
                        var ratingSort = users.OrderByDescending(x => x.Rating).ToList();
                        var user = ratingSort.Find(x => x.id == message.From.Id);
                        string ratingString = "Топ людей по рейтингу(макс. балл - 10):\n🥇" + ratingSort[0].FirstName +
                        " - " + Math.Round((double)ratingSort[0].Rating * 10, 2) + "\n🥈" + ratingSort[1].FirstName +
                        " - " + Math.Round((double)ratingSort[1].Rating * 10, 2) + "\n🥉" + ratingSort[2].FirstName +
                        " - " + Math.Round((double)ratingSort[2].Rating * 10, 2) + "\n...\n" + "Вы на " + (ratingSort.IndexOf(user) + 1) + " месте. Рейтинг - " + Math.Round((double)user.Rating,2);
                        var karmaSort = users.OrderByDescending(x => x.Karma).ToList();
                        string karmaString = "\n\nТоп людей по карме(больше - лучше):\n🥇" + karmaSort[0].FirstName +
                        " - " + Math.Round((double)karmaSort[0].Karma, 2) + "\n🥈" + karmaSort[1].FirstName +
                        " - " + Math.Round((double)karmaSort[1].Karma, 2) + "\n🥉" + karmaSort[2].FirstName +
                        " - " + Math.Round((double)karmaSort[2].Karma, 2) + "\n...\n" + "Вы на " + (karmaSort.IndexOf(user) + 1) + " месте. Карма - " + Math.Round((double)user.Karma, 2);
                        await botClient.SendTextMessageAsync(message.Chat, ratingString + karmaString,null,null,null,null,null,null,Keyboards.showMainKeyboard());
                        return;
                    }

                    if(message.Text == "⌨️Заполнить полностью")
                    {
                        Registration reg = new Registration();
                        reg.stage = 1;
                        reg.newUser = currentUser;
                        NewRegistr.Add(reg);
                        if (reg.newUser.FirstName != null)
                        {
                            KeyboardButton kb = new KeyboardButton(reg.newUser.FirstName);
                            keyboard = new ReplyKeyboardMarkup(kb);
                            keyboard.ResizeKeyboard = true;
                            await botClient.SendTextMessageAsync(message.Chat, "Для выхода напишите \"Меню\"\nВведите имя:", ParseMode.Html, null, false, false, 0, true, keyboard);
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Для выхода напишите \"Меню\"\nВведите имя:", ParseMode.Html, null, false, false, 0, true, rr);
                        }
                        return;
				    }
                    Registration newReg = NewRegistr.Find((x) => x.newUser.id == message.From.Id);
                    if (message.Text == "✏️Изменить текст")
                    {
                        newReg = new Registration();
                        newReg.stage = 7;
                        newReg.newUser = currentUser;
                        NewRegistr.Add(newReg);
                        await botClient.SendTextMessageAsync(message.Chat, "Введите новое описание:", ParseMode.Html, null, false, false, 0, true, Keyboards.showDontTouchB());
                        return;
                    }
                    if(message.Text == "🖼️Изменить фото/видео")
                    {
                        newReg = new Registration();
                        newReg.stage = 8;
                        newReg.newUser = currentUser;
                        NewRegistr.Add(newReg);
                        await botClient.SendTextMessageAsync(message.Chat, "Отправь новую фотографию/видео:", ParseMode.Html, null, false, false, 0, true, Keyboards.showDontTouchB());
                        return;
                    }
                    if(message.Text == "❎Оставить" && newReg != null && (newReg.stage == 7 || newReg.stage == 8))
                    {
                        if(currentUser.photo == null)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Фотография не может быть пустая");
                            return;
						}
                        updateUser(currentUser, newReg.newUser, message.From.Username);
                        ModelU.GetContext().SaveChanges();
                        NewRegistr.Remove(newReg);
                        sendProfile(botClient, currentUser, message.Chat);
                        return;
                    }
                    if (message.Text == "🎛Меню" || message.Text == "Меню")
                    {
                        if(newReg != null)
                        {
                            NewRegistr.Remove(newReg);
                        }
                        await botClient.SendTextMessageAsync(message.Chat, "Меню:", ParseMode.Html, null, false, false, 0, true, Keyboards.showMainKeyboard());

                        SelectSection ss = SelectSections.Find(x => x.id == currentUser.id);
                        if(ss != null)
                        {
                            SelectSections.Remove(ss);
                        }
                        return;
                    }
                    if (newReg != null)
                    {
                        newReg.stage++;
                        switch (newReg.stage)
                        {
                            case 2:
                                newReg.newUser.FirstName = message.Text;
                                if (newReg.newUser.Age != null)
                                {
                                    KeyboardButton kb = new KeyboardButton(newReg.newUser.Age.ToString());
                                    keyboard = new ReplyKeyboardMarkup(kb);
                                    keyboard.ResizeKeyboard = true;

                                    await botClient.SendTextMessageAsync(message.Chat, "Укажите возраст:", ParseMode.Html, null, false, false, 0, true, keyboard);
                                }
                                else
                                {
                                    await botClient.SendTextMessageAsync(message.Chat, "Укажите возраст:", ParseMode.Html, null, false, false, 0, true, rr);
                                }
                                return;
                            case 3:
                                try
                                {
                                    if (Convert.ToInt16(message.Text) > 99 && Convert.ToInt16(message.Text) < 14)
                                    {
                                        await botClient.SendTextMessageAsync(message.Chat, "Укажите возраст в диапазоне от 14 до 99");
                                        newReg.stage--;
                                        return;
                                    }
                                }
                                catch
                                {
                                    await botClient.SendTextMessageAsync(message.Chat, "Возраст должен быть цифрой");
                                    newReg.stage--;
                                    return;
                                }
                                
                                newReg.newUser.Age = Convert.ToInt16(message.Text);
                                if (newReg.newUser.City != null)
                                {
                                    KeyboardButton kb = new KeyboardButton(newReg.newUser.City);
                                    keyboard = new ReplyKeyboardMarkup(kb);
                                    keyboard.ResizeKeyboard = true;
                                    await botClient.SendTextMessageAsync(message.Chat, "Укажите город:", ParseMode.Html, null, false, false, 0, true, keyboard);
                                }
                                else
                                {
                                    await botClient.SendTextMessageAsync(message.Chat, "Укажите город:", ParseMode.Html, null, false, false, 0, true, rr);
                                }
                                return;
                            case 4:
                                string accesSymb = "йцукенгшщзхъфывапролдячсмитьбюжэ" + "йцукенгшщзхъфывапролдячсмитьбюжэ".ToUpper();
                                for(int i = 0; i < message.Text.Length; i++)
                                {
                                    if (!accesSymb.Contains(message.Text.Substring(i, 1)))
                                    {
                                        await botClient.SendTextMessageAsync(message.Chat, "Город должен содержать только буквы");
                                        newReg.stage--;
                                        return;
                                    }
                                }
                                newReg.newUser.City = message.Text;
                                if (newReg.newUser.Description != null)
                                {
                                    await botClient.SendTextMessageAsync(message.Chat, "Добавьте описание:", ParseMode.Html, null, false, false, 0, true, Keyboards.showDontTouchB());
                                }
                                else
                                {
                                    await botClient.SendTextMessageAsync(message.Chat, "Добавьте описание:", ParseMode.Html, null, false, false, 0, true, rr);
                                }
                                return;
                            case 5:
                                if(message.Text != "❎Оставить")
                                {
                                    newReg.newUser.Description = message.Text;
                                }

                                keyboard = Keyboards.showSexKeyboard();
                                await botClient.SendTextMessageAsync(message.Chat, "Выберите свой пол:", ParseMode.Html, null, false, false, 0, true, keyboard);
                                return;
                            case 6:
                                if(message.Text.Substring(0) != "Мужской♂" && message.Text.Substring(0) != "Женский♀")
                                {
                                    await botClient.SendTextMessageAsync(message.Chat, "Укажите корректный пол");
                                    newReg.stage--;
                                    return;
                                }
                                newReg.newUser.Sex = message.Text;
                                keyboard = Keyboards.showSexKeyboard();
                                await botClient.SendTextMessageAsync(message.Chat, "Выберите пол анкет:", ParseMode.Html, null, false, false, 0, true, keyboard);
                                return;
                            case 7:
                                if (message.Text.Substring(0) != "Мужской♂" && message.Text.Substring(0) != "Женский♀")
                                {
                                    await botClient.SendTextMessageAsync(message.Chat, "Укажите корректный пол");
                                    newReg.stage--;
                                    return;
                                }
                                newReg.newUser.TargetSex = message.Text;
                                await botClient.SendTextMessageAsync(message.Chat, "Загрузите фото/видео:", ParseMode.Html, null, false, false, 0, true, Keyboards.showDontTouchB());
                                return;
                            case 8:
                                await botClient.SendTextMessageAsync(message.Chat, "Описание обновлено!", ParseMode.Html, null, false, false, 0, true, Keyboards.showProfileKeyboard());
                                newReg.newUser.Description = message.Text;
                                currentUser.Description = message.Text;
                                ModelU.GetContext().SaveChanges();
                                NewRegistr.Remove(newReg);
                                sendProfile(botClient, currentUser, message.Chat);
                                return;
                        }
                        return;
                    }

                    //обработка чисел
                    else
                    {
                        SelectSection ss = SelectSections.Find(x => x.id == currentUser.id);
                        if (ss != null)
                        {
                            if(int.TryParse(message.Text, out int result))
                            {
                                Section selected = ModelU.GetContext().Section.Find(result);
                                if (selected == null)
                                {
                                    await botClient.SendTextMessageAsync(message.Chat, "Такого раздела нет");
                                    return;
                                }
                                string currentSection = "" + selected.id + ". " + selected.Name + "\nСоздатель - " + ModelU.GetContext().Users.Find(selected.Creator).FirstName + "\nПросмотров - " + selected.Popularity
                                + "\nКоличество анкет: " + ModelU.GetContext().SectionAnkets.Where(x => x.SectionId == selected.id).Count();

                                var paramsList = ModelU.GetContext().SectionParams
	                                .Where(x => x.SectionID == selected.id).ToList();
                                string stringPars = "\nПараметры:";
                                int count = 1;
                                foreach (var x in paramsList)
                                {
	                                stringPars += "\n" + count + ". " + x.ParamName;
	                                count++;
                                }
                                currentSection += stringPars;
                                await botClient.SendTextMessageAsync(message.Chat, currentSection, null, null, null, null, null, null, Keyboards.showSectionWebKeyboard());
                            }
                            else
                            {
                                await botClient.SendTextMessageAsync(message.Chat, "Номер не может быть строкой");
                            }
                            return;
                        }
                        await botClient.SendTextMessageAsync(message.Chat, "Такая команда не существует либо еще в разработке.", ParseMode.Html, null, false, false, 0, true, keyboard);
                        return;
                    }
                    
                }
                if(message.Type == MessageType.Document && message.Document.MimeType == "image/jpeg")
                {
                    Registration newReg = NewRegistr.Find((x) => x.newUser.id == message.From.Id);
                    if (newReg != null)
                    {
                        var f = await botClient.GetFileAsync(message.Document.FileId);

                        string fileExt = f.FilePath.Split('.')[1];
                        System.IO.FileStream fs = System.IO.File.Create(AppDomain.CurrentDomain.BaseDirectory + "/photos/" + message.Document.FileId + "." + fileExt);
                        await botClient.DownloadFileAsync(f.FilePath, fs);
                        fs.Close();
                        newReg.newUser.photo = "Doc" + message.Document.FileId + "." + fileExt;
                        updateUser(currentUser, newReg.newUser, message.From.Username);
                        ModelU.GetContext().SaveChanges();

                        NewRegistr.Remove(newReg);
                        sendProfile(botClient, currentUser, message.Chat);
                        return;
                    }
                    //Console.Write("Документ\n");
                }
                if(message.Type == MessageType.Photo)
                {
                    Registration newReg = NewRegistr.Find((x) => x.newUser.id == message.From.Id);
                    if (newReg != null)
                    {
                        newReg.newUser.photo = message.Photo[0].FileId;
                        updateUser(currentUser,newReg.newUser, message.From.Username);
                        ModelU.GetContext().SaveChanges();
                        NewRegistr.Remove(newReg);
                        sendProfile(botClient, currentUser, message.Chat);
                        return;
                    }
                    AnonimChat activeChat = AnonimChats.Find(x => x.user1 == message.From.Id || x.user2 == message.From.Id);
                    if (activeChat != null)
                    {
                        if (activeChat.user1 == 0 || activeChat.user2 == 0)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Собеседник пока не найден😔");
                            return;
                        }
                        if (activeChat.user1 == message.From.Id)
                        {
                            await botClient.SendPhotoAsync(activeChat.user2, message.Photo[0].FileId);
                            return;
                        }
                        if (activeChat.user2 == message.From.Id)
                        {
                            await botClient.SendPhotoAsync(activeChat.user1, message.Photo[0].FileId);
                            return;
                        }
                    }
                    //Console.Write("Картика\n");
                }
                if(message.Type == MessageType.Video)
                {
                    Registration newReg = NewRegistr.Find((x) => x.newUser.id == message.From.Id);
                    if (newReg != null)
                    {
                        newReg.newUser.photo = "Vid" + message.Video.FileId;
                        updateUser(currentUser, newReg.newUser, message.From.Username);
                        ModelU.GetContext().SaveChanges();

                        NewRegistr.Remove(newReg);
                        sendProfile(botClient, currentUser, message.Chat);
                        return;
                    }
                    //Console.Write("Видео\n");
                }
                if(message.Type == MessageType.Sticker)
                {
                    AnonimChat activeChat = AnonimChats.Find(x => x.user1 == message.From.Id || x.user2 == message.From.Id);
                    if (activeChat != null)
                    {
                        if (activeChat.user1 == 0 || activeChat.user2 == 0)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Собеседник пока не найден😔");
                            return;
                        }
                        if (activeChat.user1 == message.From.Id)
                        {
                            await botClient.SendStickerAsync(activeChat.user2, message.Sticker.FileId);
                            return;
                        }
                        if (activeChat.user2 == message.From.Id)
                        {
                            await botClient.SendStickerAsync(activeChat.user1, message.Sticker.FileId);
                            return;
                        }
                    }
                }
            }
        }
        public async static void generateNewChat(ITelegramBotClient botClient, long caller, string add)
        {
            AnonimChat chat = AnonimChats.Find(x => x.user1 == 0 || x.user2 == 0);
            if (chat == null)
            {
                AnonimChat newChat = new AnonimChat();
                newChat.user1 = caller;
                AnonimChats.Add(newChat);
                await botClient.SendTextMessageAsync(caller, add + "Ждем собеседника", null, null, null, null, null, null, Keyboards.showAnonimChatKeyboard());
                return;
            }
            if (chat.user1 == 0)
            {
                chat.user1 = caller;
                await botClient.SendTextMessageAsync(chat.user2, "Нашли собеседника. Приступай к общению🙂");
            }
            if (chat.user2 == 0)
            {
                chat.user2 = caller;
                await botClient.SendTextMessageAsync(chat.user1, "Нашли собеседника. Приступай к общению🙂");
            }
            await botClient.SendTextMessageAsync(caller, add + "Нашли собеседника. Приступай к общению🙂", null, null, null, null, null, null, Keyboards.showAnonimChatKeyboard());
            return;
        }
        public async static void sendNewAnket(ITelegramBotClient botClient, Message message, Users current)
        {   
            List<MessagesStack> stackGetter = MS.Where(x => x.getter == message.From.Id).ToList();
            if (stackGetter.Count != 0)
            {
                sendAnketFromStack(botClient, message, stackGetter[0]);
                return;
            }
            //List<Users> us = ModelU.GetContext().Users.Where((x) => x.Active == true && x.Sex == current.TargetSex && x.TargetSex == current.Sex).ToList();
            List<Users> us = ModelU.GetContext().Users.Where((x) => x.Active == true).ToList();
            us.Remove(us.Find((x) => x.id == message.From.Id));
            foreach (var x in Viewed.Where((x) => x.user1 == message.From.Id).ToList())
            {
                us.Remove(us.Find(i => i.id == x.user2));
            }
            int count = us.Count;
            if (count == 0)
            {
                await botClient.SendTextMessageAsync(message.Chat, "Анкеты закончились, приходи позже😔", ParseMode.Html, null, false, false, 0, true, Keyboards.showMainKeyboard());
                return;
            }
            Users anketa = us[rnd.Next(count - 1)];
            AnketsSend AS = new AnketsSend()
            {
                user1 = message.From.Id,
                user2 = anketa.id
            };
            editView(anketa.id);
            Viewed.Add(AS);
            if (anketa.photo.Substring(0, 3) == "Doc")
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "/photos/" + anketa.photo.Substring(3);
                System.IO.FileStream fs = System.IO.File.OpenRead(path);
                await botClient.SendPhotoAsync(message.Chat, new Telegram.Bot.Types.InputFiles.InputOnlineFile(fs), "⭐Имя: " + anketa.FirstName + "\n📅Возраст: " + anketa.Age + "\n🏢Город: " + anketa.City +
               "\n\n" + anketa.Description, null, null, null, null, null, Keyboards.showAnketaMarkKeyboard());
                fs.Close();
            }
            else if (anketa.photo.Substring(0, 3) == "Vid")
            {
                await botClient.SendVideoAsync(message.Chat, anketa.photo.Substring(3), null, null, null, null, "⭐Имя: " + anketa.FirstName + "\n📅Возраст: " + anketa.Age + "\n🏢Город: " + anketa.City +
               "\n\n" + anketa.Description, null, null, null, null, null, null, Keyboards.showAnketaMarkKeyboard());
            }
            else
            {
                await botClient.SendPhotoAsync(message.Chat, anketa.photo, "⭐Имя: " + anketa.FirstName + "\n📅Возраст: " + anketa.Age + "\n🏢Город: " + anketa.City +
                "\n\n" + anketa.Description, null, null, null, null, null, Keyboards.showAnketaMarkKeyboard());
            }
            return;
        }
        public async static void sendAnketFromStack(ITelegramBotClient botClient, Message message, MessagesStack msSend)
        {
            Users anketa = ModelU.GetContext().Users.Find(msSend.sender);

			if (anketa.photo.Substring(0, 3) == "Doc")
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "/photos/" + anketa.photo.Substring(3);
                System.IO.FileStream fs = System.IO.File.OpenRead(path);
                await botClient.SendPhotoAsync(message.Chat, new Telegram.Bot.Types.InputFiles.InputOnlineFile(fs), "Кому-то понравилась ваша анкета♥️\n⭐Имя: " + anketa.FirstName + "\n📅Возраст: " + anketa.Age + "\n🏢Город: " + anketa.City +
                "\n\n" + anketa.Description, null, null, null, null, null, Keyboards.showAnketaMarkKeyboard());
                fs.Close();
            }
            else if (anketa.photo.Substring(0, 3) == "Vid")
            {
                await botClient.SendVideoAsync(message.Chat, anketa.photo.Substring(3), null,null,null,null,"Кому-то понравилась ваша анкета♥️\n⭐Имя: " + anketa.FirstName + "\n📅Возраст: " + anketa.Age + "\n🏢Город: " + anketa.City +
                "\n\n" + anketa.Description, null, null, null, null, null,null, Keyboards.showAnketaMarkKeyboard());
            }
            else
            {
                await botClient.SendPhotoAsync(message.Chat, anketa.photo, "Ваша анкета кому-то понравилась♥️:\n⭐Имя: " + anketa.FirstName + "\n📅Возраст: " + anketa.Age + "\n🏢Город: " + anketa.City +
                "\n\n" + anketa.Description, null, null, null, null, null, Keyboards.showAnketaMarkKeyboard());
            }
            AnketsSend AS = new AnketsSend()
            {
                user1 = message.From.Id,
                user2 = anketa.id
            };
            editView(anketa.id);
            Viewed.Add(AS);
            msSend.type = 1;
            return;
        }
        public static void editLike(long id)
        {
            LikedUsers likU = LU.Find(x => x.userid == id);
            if (likU != null)
            {
                likU.liked++;
            }
            else
            {
                Users current = ModelU.GetContext().Users.Find(id);
                likU = new LikedUsers();
                likU.userid = id;
                likU.viewed = (int)current.ViewCount;
                likU.liked = (int)current.LikedCount + 1;
                LU.Add(likU);
            }
        }
        public static void editView(long id)
        {
            LikedUsers likU = LU.Find(x => x.userid == id);
            if (likU != null)
            {
                likU.viewed++;
            }
            else
            {
                Users current = ModelU.GetContext().Users.Find(id);
                likU = new LikedUsers();
                likU.userid = id;
                likU.viewed = (int)current.ViewCount + 1;
                likU.liked = (int)current.LikedCount;
                LU.Add(likU);
            }
        }
        public static void editKarma(long id, double value)
        {
            UsersKarma usK = UK.Find(x => x.user == id);
            if(usK != null)
            {
                usK.karma = usK.karma + value;
			}
            else
            {
                usK = new UsersKarma();
                usK.user = id;
                usK.karma = (double)ModelU.GetContext().Users.Find(id).Karma + value;
                UK.Add(usK);
			}
		}

        public static void updateUser(Users oldD, Users newD, string username)
        {
            if (username != null) oldD.username = username;
            else username = oldD.id.ToString();
            oldD.photo = newD.photo;
            oldD.FirstName = newD.FirstName;
            oldD.Age = newD.Age;
            oldD.City = newD.City;
            try
            {
                oldD.TargetSex = newD.TargetSex.Substring(0, 7);
                oldD.Sex = newD.Sex.Substring(0, 7);
            }
            catch
            {}
            oldD.Description = newD.Description;
            oldD.Active = true;
            oldD.VIP = false;
        }

        public static async void sendProfile(ITelegramBotClient botClient, Users Current, Chat chat)
        {
            ReplyKeyboardMarkup keyboard = Keyboards.showProfileKeyboard();
            if (Current.photo == null)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "/photos/" + "unnamed.jpg";
                System.IO.FileStream fs = System.IO.File.OpenRead(path);
                await botClient.SendPhotoAsync(chat, new Telegram.Bot.Types.InputFiles.InputOnlineFile(fs), getDescript(Current), null, null, null, null, null, keyboard);
                fs.Close();
            }
            else if (Current.photo.Substring(0, 3) == "Doc")
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "/photos/" + Current.photo.Substring(3);
                System.IO.FileStream fs = System.IO.File.OpenRead(path);
                await botClient.SendPhotoAsync(chat, new Telegram.Bot.Types.InputFiles.InputOnlineFile(fs), getDescript(Current), null, null, null, null, null, keyboard);
                fs.Close();
            }
            else if (Current.photo.Substring(0, 3) == "Vid")
            {
				await botClient.SendVideoAsync(chat, Current.photo.Substring(3), null, null, null, null, getDescript(Current), null, null, null, null, null, null, keyboard);
			}
            else
            {
                await botClient.SendPhotoAsync(chat, Current.photo, getDescript(Current), null, null, null, null, null, keyboard);
            }
            return;
        }

        public static string getDescript(Users Current)
        {
            return "⭐Имя: " + Current.FirstName + "\n📅Возраст: " + Current.Age + "\n🏢Город: " + Current.City +
                "\n👩🏻‍‍👨🏻Пол анкет: " + Current.TargetSex +"\n✨Карма: " + Current.Karma + "\n📊Рейтинг: " + (Current.Rating * 10) + "\n👑VIP статус: " + getVipInString(Current.VIP) + "\n\n" + Current.Description;
		}
        public static string getVipInString(bool vip)
        {
            if (vip)
            {
                return "Активен";
            }
            else return "Неактивен";
		}

        static void Main(string[] args)
        {
            Console.WriteLine("Запуск...");
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
                );
			anketsTotal(bot);
            karmaTotal();
            lvTotal();
            while (true)
            {
                string[] command = Console.ReadLine().Split(' ');
                if (command[0] == "/send")
                {
                    bot.SendTextMessageAsync(command[1], command[2]);//ParseEmoji("U+1F601"));
                }
                if (command[0] == "/sendall")
                {
                    
                }
                if (command[0] == "/getphoto")
                {
                    Users curr = ModelU.GetContext().Users.Find(Convert.ToInt32(command[1]));
                    try
                    {
                        getPhotoT(curr.photo, bot, Convert.ToInt32(command[1]));
                        Console.WriteLine("Файл выгружен");
                    }
                    catch
                    {
                        Console.WriteLine("Не получилось выгрузить файл");
                    }
                }
                if(command[0] == "/syncrating")
                {
                    foreach (var x in ModelU.GetContext().Users.ToList())
                    {
                        x.Rating = (float)Math.Round((float)x.LikedCount / (float)x.ViewCount,2);
                        if (float.IsNaN((float)x.Rating))
                        {
                            x.Rating = 0;
                        }
                    }
                    ModelU.GetContext().SaveChanges();
                    Console.WriteLine("Done!");
                }
            }
        }
        public async static void getPhotoT(string fileid, ITelegramBotClient botClient, long userid)
        {
            var f = await botClient.GetFileAsync(fileid);

            string fileExt = f.FilePath.Split('.')[1];
            System.IO.FileStream fs = System.IO.File.Create(AppDomain.CurrentDomain.BaseDirectory + "/photos/" + userid + "." + fileExt);
            await botClient.DownloadFileAsync(f.FilePath, fs);
            fs.Close();
        }
        public async static Task anketsTotal(ITelegramBotClient botClient)
        {
            await Task.Delay(3600000 * 24);
            Viewed.Clear();
            int hour = DateTime.Now.Hour;
            if(hour > 22 || hour < 8)
            {
                return;
			}
			List<Users> allUsers = ModelU.GetContext().Users.Where(x => x.Active == true).ToList();
			foreach (var x in allUsers)
			{
				List<MessagesStack> UsStack = MS.Where(m => m.getter == x.id).ToList();
				if (UsStack.Count != 0)
				{
					await botClient.SendTextMessageAsync(x.id, "Вас оценило " + UsStack.Count + " человек(а). Нажми ❤Оценивать для просмотра!", null, null, null, null, null, null, Keyboards.showMainKeyboard());
                    await Task.Delay(100);
				}
			}
			await anketsTotal(botClient);
        }
        public async static Task karmaTotal()
        {
            await Task.Delay(60000);
            foreach(var x in UK)
            {
                Users user = await ModelU.GetContext().Users.FindAsync(x.user);
                user.Karma = (float)x.karma;
			}
            UK.Clear();
            ModelU.GetContext().SaveChanges();
            await karmaTotal();
        }
        public async static Task lvTotal()
        {
            await Task.Delay(60000);
            foreach (var x in LU)
            {
                Users user = await ModelU.GetContext().Users.FindAsync(x.userid);
                user.LikedCount = x.liked;
                user.ViewCount = x.viewed;
                user.Rating = (float)Math.Round((float)user.LikedCount / (float)user.ViewCount, 2);
                if (float.IsNaN((float)user.Rating))
                {
                    user.Rating = 0;
                }
            }
            LU.Clear();
            ModelU.GetContext().SaveChanges();
            await lvTotal();
        }
    }
}