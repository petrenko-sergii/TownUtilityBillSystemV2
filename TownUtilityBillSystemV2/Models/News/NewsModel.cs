using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace TownUtilityBillSystemV2.Models.News
{
	public class NewsModel 
	{
		#region Properties

		public News SingleNews;
		public List<News> NewsList;
		public NewsEmployee NewsEmployee;

		#endregion

		#region Ctor

		public NewsModel()
		{
			SingleNews = new News();
			NewsList = new List<News>();
			NewsEmployee = new NewsEmployee();
			SingleNews.NewsChapters = new List<NewsChapter>();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Rerurns news titles list for a slide-show
		/// </summary>
		public void GetNewsTitlesForSlideShow()
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var newsDB = context.NEWS.ToList();
				var newsTitlesDB = context.NEWS_TITLEs.ToList();
				string currentLanguage = GetCurrentLanguage();

				foreach (var n in newsDB)
				{
					string newsTitle = newsTitlesDB.
						Where(t=>t.NEWS_ID == n.ID).
						Where(t=>t.LANGUAGE == currentLanguage).
						FirstOrDefault().TITLE;
					
					NewsList.Add(new News() { Id = n.ID, Date = n.DATE, Title = newsTitle, ImagePath = GetNewsImage(n.IMAGE_ID) });
				}
			}
		}

		public void GetSingleNews(int newsId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				var singleNewsDB = context.NEWS.Find(newsId);
				string currentLanguage = GetCurrentLanguage();

				var newsTitle = context.NEWS_TITLEs.
					Where(t=>t.NEWS_ID == singleNewsDB.ID).
					Where(t => t.LANGUAGE == currentLanguage).
					FirstOrDefault().TITLE;

				var chaptersDB = context.NEWS_CHAPTERs.
					Where(c => c.NEWS_ID == newsId).
					Where(c => c.LANGUAGE == currentLanguage).
					ToList();

				SingleNews.Title = newsTitle;
				SingleNews.Date = singleNewsDB.DATE;

				foreach (var ch in chaptersDB)
					SingleNews.NewsChapters.Add(new NewsChapter() { Id = ch.ID, Text = ch.TEXT });
			}
		}

		public string GetNewsImage(int? imageId)
		{
			using (var context = new TownUtilityBillSystemV2Entities())
			{
				string imageName = "";
				string imagePathForHtml = "";
				string imagePathDB = "";
				string folderName = "";

				var imageDB = context.IMAGE_NEWS.Find(imageId) ?? null;

				if (imageDB != null)
				{
					imagePathDB = imageDB.PATH.ToString();
					folderName = Path.GetFileName(Path.GetDirectoryName(imagePathDB));
					imageName = Path.GetFileName(imagePathDB);
					imagePathForHtml = "/Content/Images/" + folderName + "/" + imageName;
				}
				return imagePathForHtml;
			}
		}

		public string GetCurrentLanguage()
		{
			return Thread.CurrentThread.CurrentCulture.ToString();
		}

		#endregion
	}
}
