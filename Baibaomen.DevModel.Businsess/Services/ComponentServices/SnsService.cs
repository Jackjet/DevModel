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
        public SnSService(string snsConfiguration) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task EmailNotifyAsync(int receiver, string subject, string content) {
            //todo: Get receiver email address; send email.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task SmsNotifyAsync(int receiver, string content) {
            //todo: Get receiver mobile; send sms.
        }
    }
}
