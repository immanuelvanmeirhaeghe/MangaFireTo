using Android.Webkit;
using Java.Net;
using System;

namespace MangaFireTo
{
    public class MangaFireWebViewClient : WebViewClient
    {
        public static readonly string MangaFireToBaseDomain = @"https://mangafire.to/";
        public static readonly string FacebookDomain = @"https://www.facebook.com/sharer.php?t=MangaFire%20-%20Read%20Manga%20Online%20Free&u=https%3A%2F%2Fmangafire.to";
        public static readonly string XDomain = @"https://twitter.com/intent/tweet?text=MangaFire%20-%20Read%20Manga%20Online%20Free&url=https%3A%2F%2Fmangafire.to";
        public static readonly string MessengerDomain = @"fb-messenger://share/?link=https%3A%2F%2Fmangafire.to&app_id=291494419107518";
        public static readonly string RedditDomain = @"https://reddit.com/submit?title=MangaFire%20-%20Read%20Manga%20Online%20Free&url=https%3A%2F%2Fmangafire.to";

        [Obsolete]
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            try
            {
                if (FacebookDomain.Equals(url, StringComparison.CurrentCultureIgnoreCase)
                    || XDomain.Equals(url, StringComparison.CurrentCultureIgnoreCase)
                    || MessengerDomain.Equals(url, StringComparison.CurrentCultureIgnoreCase)
                    || RedditDomain.Equals(url, StringComparison.CurrentCultureIgnoreCase)
                    ) 
                {
                    view.LoadUrl(url);
                    return false;
                }

                string sanUrl = URLEncoder.Encode(url);
                if (string.IsNullOrWhiteSpace(sanUrl)
                    || !URLUtil.IsValidUrl(URLDecoder.Decode(sanUrl))
                    || !URLUtil.IsHttpsUrl(URLDecoder.Decode(sanUrl))
                    || !sanUrl.StartsWith(URLEncoder.Encode(MangaFireToBaseDomain)))
                {
                    return true;
                }

                view.LoadUrl(URLDecoder.Decode(sanUrl));
                return false;
            }
            catch (Exception)
            {
                view.LoadUrl(MangaFireToBaseDomain);
                return false;
            }
        }
    }
}