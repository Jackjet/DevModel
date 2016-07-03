using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess.ComponentServices
{
    /// <summary>
    /// 
    /// </summary>
    public class SnSService
    {
        Func<int, string> _emailResolver, _mobileResolver;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="snsConfiguration"></param>
        /// <param name="emailResolver"></param>
        /// <param name="mobileResolver"></param>
        public SnSService(string snsConfiguration,Func<int,string> emailResolver,Func<int,string> mobileResolver) {
            _emailResolver = emailResolver;
            _mobileResolver = mobileResolver;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiverId"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task EmailNotifyAsync(int receiverId, string subject, string content) {
            var email = _emailResolver(receiverId);
            //todo: send email.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiverId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task SmsNotifyAsync(int receiverId, string content) {
            var mobile = _mobileResolver(receiverId);
            //todo: send sms.
        }
    }
}
