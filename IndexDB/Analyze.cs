using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IndexDB
{
	public class Analyze
	{
		private string _dicDir = "";	// 辞書DBのパス

		public Analyze(string dicDir)
		{
			this._dicDir = dicDir;
		}

		public ArrayList Execute(string sentence)
		{
			if (this._dicDir == "")
			{
				return null;
			}

			NMeCab.MeCabParam param = new NMeCab.MeCabParam();
			param.DicDir = this._dicDir;
			param.LatticeLevel = NMeCab.MeCabLatticeLevel.One;

			NMeCab.MeCabTagger mecabTagger = NMeCab.MeCabTagger.Create(param);
			string message = mecabTagger.ParseNBest(2, sentence);

			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string m in message.Split('\n'))
			{
				try
				{
					// 名詞のみ対象
					if (m.Split('\t')[1].IndexOf("名詞") == 0)
					{
						dictionary.Add(m.Split('\t')[0], "A");
					}
				}
				catch
				{
				}
			}

			ArrayList wordList = new ArrayList();
			foreach (string key in dictionary.Keys)
			{
				wordList.Add(key);
			}

			return wordList;
		}

	}
}
