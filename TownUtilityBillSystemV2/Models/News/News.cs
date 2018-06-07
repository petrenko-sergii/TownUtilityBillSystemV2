using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownUtilityBillSystemV2.Models.BaseModels;

namespace TownUtilityBillSystemV2.Models.News
{
	public class News : ObjectWithImage
	{
		public string Title { get; set; }
		public DateTime Date { get; set; }

		public List<NewsChapter> NewsChapters { get; set; }
	}
}