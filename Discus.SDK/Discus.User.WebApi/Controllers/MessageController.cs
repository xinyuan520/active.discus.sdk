namespace Discus.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// 发布消息测试
        /// </summary>
        /// <returns></returns>
        [Route("Publish"), HttpGet]
        public async Task Publish()
        {
            await _messageService.Publish();
        }
    }
}
