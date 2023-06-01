using Telegram.Bot;
using Telegram.Bot.Polling;

namespace RestuarantManager.Telegram
{
    public class TelegramHandler : IUpdateHandler
    {

        Task IUpdateHandler.HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task IUpdateHandler.HandleUpdateAsync(ITelegramBotClient botClient, global::Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
