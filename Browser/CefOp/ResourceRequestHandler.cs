using CefSharp;
using CefSharp.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser.CefOp
{
	public class Cef_ResRequestHandler : ResourceRequestHandler
	{

		/// <summary>
		/// 特定の通信をブロックします。
		/// </summary>
		protected override CefReturnValue OnBeforeResourceLoad(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
		{
			// ログイン直後に勝手に遷移させられ、ブラウザがホワイトアウトすることがあるためブロックする
			if (request.Url.Contains(@"/rt.gsspat.jp/"))
			{
				return CefReturnValue.Cancel;
			}
			// remove range request to allow bgm cachings
			if (request.Url.Contains(@"kcs2/resources/bgm"))
			{
				var headers = request.Headers;
				headers.Remove("Range");
				request.Headers = headers;
			}
			return base.OnBeforeResourceLoad(chromiumWebBrowser, browser, frame, request, callback);
		}

	}
}
