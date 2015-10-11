using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIP.Messages
{
	public class CloseWindowMessage : MessageBase
	{
		public Action<object> OnClosed { get; set; }
	}


}
