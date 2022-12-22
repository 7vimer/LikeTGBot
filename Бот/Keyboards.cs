using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace Бот
{
	public class Keyboards
	{
        public static ReplyKeyboardMarkup showDontTouchB()
        {
            KeyboardButton kb = new KeyboardButton("❎Оставить");
            var keyboard = new ReplyKeyboardMarkup(kb);
            keyboard.ResizeKeyboard = true;
            return keyboard;
        }
        public static ReplyKeyboardMarkup showCancelB()
        {
            KeyboardButton kb = new KeyboardButton("❎Отмена");
            var keyboard = new ReplyKeyboardMarkup(kb);
            keyboard.ResizeKeyboard = true;
            return keyboard;
        }
        public static ReplyKeyboardMarkup showMainKeyboard()
        {
            var keyBut = new KeyboardButton("");
            var keyboard = new ReplyKeyboardMarkup(keyBut)
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("👤Мой профиль"),
                        new KeyboardButton("♥️Оценивать"),
                        new KeyboardButton("🎆WEB 2.0")
                    },
                    new[]
                    {
                        new KeyboardButton("🏆Лидеры"),
                        new KeyboardButton("⚙Информация"),
                        new KeyboardButton("🛒О подписке")
                    },
                    new[]
                    {
                        new KeyboardButton("👥Анонимный чат")
                    }
                },
                ResizeKeyboard = true
            };
            return keyboard;
        }

        public static ReplyKeyboardMarkup showMainWebKeyboard()
        {
            var keyBut = new KeyboardButton("");
            var keyboard = new ReplyKeyboardMarkup(keyBut)
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("📄Разделы"),
                        new KeyboardButton("📑Мои разделы")
                    },
                    new[]
                    {
                        new KeyboardButton("🎛Меню")
                    }
                },
                ResizeKeyboard = true
            };
            return keyboard;
        }

        public static ReplyKeyboardMarkup showMyWebKeyboard()
        {
            var keyBut = new KeyboardButton("");
            var keyboard = new ReplyKeyboardMarkup(keyBut)
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("Создать раздел✅"),
                        new KeyboardButton("Удалить раздел❌")
                    },
                    new[]
                    {
                        new KeyboardButton("🎛Меню")
                    }
                },
                ResizeKeyboard = true
            };
            return keyboard;
        }
        public static ReplyKeyboardMarkup showCreatingSectionKeyboard()
        {
	        var keyBut = new KeyboardButton("");
	        var keyboard = new ReplyKeyboardMarkup(keyBut)
	        {
		        Keyboard = new[]
		        {
			        new[]
			        {
				        new KeyboardButton("Готово✅"),
				        new KeyboardButton("Отмена❌")
					}
		        },
		        ResizeKeyboard = true
	        };
	        return keyboard;
        }
        public static ReplyKeyboardMarkup showSectionWebKeyboard()
        {
            var keyBut = new KeyboardButton("");
            var keyboard = new ReplyKeyboardMarkup(keyBut)
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("Оценивать анкеты♥️"),
                        new KeyboardButton("Создать анкету✅")
                    },
                    new[]
                    {
                        new KeyboardButton("🎛Меню")
                    }
                },
                ResizeKeyboard = true
            };
            return keyboard;
        }
        public static ReplyKeyboardMarkup showProfileKeyboard()
        {
            var keyBut = new KeyboardButton("");
            var keyboard = new ReplyKeyboardMarkup(keyBut)
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("⌨️Заполнить полностью"),
                        new KeyboardButton("✏️Изменить текст")
                    },
                    new[]
                    {
                        new KeyboardButton("🖼️Изменить фото/видео")
                    },
                    new[]
                    {
                        new KeyboardButton("🔌Отключить анкету")
                    },
                    new[]
                    {
                        new KeyboardButton("🎛Меню")
                    }
                },
                ResizeKeyboard = true
            };
            return keyboard;
        }
        public static ReplyKeyboardMarkup showSexKeyboard()
        {
            var keyBut = new KeyboardButton("");
            var keyboard = new ReplyKeyboardMarkup(keyBut)
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("Мужской♂"),
                        new KeyboardButton("Женский♀")
                    }
                },
                ResizeKeyboard = true
            };
            return keyboard;
        }
        public static ReplyKeyboardMarkup showAnketaMarkKeyboard()
        {
            var keyBut = new KeyboardButton("");
            var keyboard = new ReplyKeyboardMarkup(keyBut)
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("♥️"),
                        new KeyboardButton("👎"),
                        new KeyboardButton("⚠️"),
                        new KeyboardButton("🛑")
                    }
                },
                ResizeKeyboard = true
            };
            return keyboard;
        }
        public static ReplyKeyboardMarkup showAnonimChatKeyboard()
        {
            var keyBut = new KeyboardButton("");
            var keyboard = new ReplyKeyboardMarkup(keyBut)
            {
                Keyboard = new[]
                {
                    new[]
                    {
                        new KeyboardButton("👤Следующий"),
                        new KeyboardButton("⚠️Жалоба"),
                        new KeyboardButton("🛑Стоп")
                    }
                },
                ResizeKeyboard = true
            };
            return keyboard;
        }
    }
}
