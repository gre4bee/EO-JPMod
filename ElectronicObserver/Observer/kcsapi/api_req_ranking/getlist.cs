﻿using ElectronicObserver.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElectronicObserver.Observer.kcsapi.api_req_ranking
{

	public class getlist : APIBase
	{
		public override void OnResponseReceived(dynamic data)
		{
			KCDatabase db = KCDatabase.Instance;

			string pattern = new StringBuilder("\"api_.{12}\":[0-9]*,\"api_.{12}\":\"").AppendFormat("{0}\"", db.Admiral.AdmiralName).ToString();
			Regex regex = new Regex(pattern);

			try
			{
				if (string.IsNullOrEmpty(rankData)) return;
				string rankData = regex.Match(data.ToString()).Value;
				rankData = rankData.Split(',')[0].Split(':')[1].Replace('"', '\0');
				db.Admiral.Senka = int.Parse(rankData);
			}
			catch (Exception ex)
			{   //ファイルがロックされている; 頻繁に出るのでエラーレポートを残さない

				Utility.Logger.Add(3, "failed to save API:"+ ex.Message);
			}

			base.OnResponseReceived((object)data);
		}

		public override string APIName => "api_req_ranking/getlist";

		public override bool IsResponseSupported => true;
	}

}