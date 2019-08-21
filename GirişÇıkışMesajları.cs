using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace DaeGirisCikisMesajlari
{
	public class GirişÇıkışMesajları : RocketPlugin<GirişÇıkışMesajlarıYapılandırma>
	{
		protected override void Load()
		{
			if (Configuration.Instance.OyuncuSayısıAktif)
            {
                foreach (var steamOyuncu in Provider.clients)
                {
                    EffectManager.sendUIEffect(Configuration.Instance.OyuncuSayısıEfektIdsi, 15964, steamOyuncu.playerID.steamID, true,
                        $"<color=#{Configuration.Instance.MevcutOyuncuSayısıRengi}>{Provider.clients.Count}</color>/<color=#{Configuration.Instance.MaksimumOyuncuSayısıRengi}>{Provider.maxPlayers}</color>");
                }
            }

            if (Configuration.Instance.OyuncuSayısıAktif || Configuration.Instance.GirişMesajlarıAktif)
            {
                U.Events.OnPlayerConnected += OyuncuBağlandığında;
            }

            if (Configuration.Instance.OyuncuSayısıAktif || Configuration.Instance.ÇıkışMesajlarıAktif)
            {
                U.Events.OnPlayerDisconnected += OyuncuAyrıldığında;
            }
        }

		protected override void Unload()
		{
			if (Configuration.Instance.OyuncuSayısıAktif || Configuration.Instance.GirişMesajlarıAktif || Configuration.Instance.ÇıkışMesajlarıAktif)
            {
                foreach (var steamOyuncu in Provider.clients)
                {
                    var steamId = steamOyuncu.playerID.steamID;

                    EffectManager.askEffectClearByID(Configuration.Instance.GirişEfektiIdsi, steamId);
                    EffectManager.askEffectClearByID(Configuration.Instance.ÇıkışEfektiIdsi, steamId);
                    EffectManager.askEffectClearByID(Configuration.Instance.OyuncuSayısıEfektIdsi, steamId);
                }
            }

            if (Configuration.Instance.OyuncuSayısıAktif || Configuration.Instance.GirişMesajlarıAktif)
            {
                U.Events.OnPlayerConnected -= OyuncuBağlandığında;
            }

            if (Configuration.Instance.OyuncuSayısıAktif || Configuration.Instance.ÇıkışMesajlarıAktif)
            {
                U.Events.OnPlayerDisconnected -= OyuncuAyrıldığında;
            }
        }

		private void OyuncuBağlandığında(UnturnedPlayer oyuncu)
		{
			foreach (var steamOyuncu in Provider.clients)
			{
			    var steamId = steamOyuncu.playerID.steamID;
				
                if (Configuration.Instance.GirişMesajlarıAktif)
                {
                    EffectManager.sendUIEffect(Configuration.Instance.GirişEfektiIdsi, 15963, steamId, true,
                        $"<color=#{Configuration.Instance.GirişMesajıRengi}>{Translate("Giriş", $"<color=#{ColorUtility.ToHtmlStringRGBA(oyuncu.Color)}>{oyuncu.CharacterName}</color>")}</color>");
                }

                if (Configuration.Instance.OyuncuSayısıAktif)
                {
                    EffectManager.sendUIEffect(Configuration.Instance.OyuncuSayısıEfektIdsi, 15964, steamId, true,
                        $"<color=#{Configuration.Instance.MevcutOyuncuSayısıRengi}>{Provider.clients.Count}</color>/<color=#{Configuration.Instance.MaksimumOyuncuSayısıRengi}>{Provider.maxPlayers}</color>");
                }
            }

            if (Configuration.Instance.ÖzelEfektlerAktif)
            {
                foreach (var efektIdsi in Configuration.Instance.EfektIdleri)
                {
                    var anahtar = efektIdsi > short.MaxValue ?
                        (short)(efektIdsi - short.MaxValue + 1) :
                        (short)efektIdsi;

                    EffectManager.sendUIEffect(efektIdsi, anahtar, oyuncu.CSteamID, true);
                }
            }
		}

		private void OyuncuAyrıldığında(UnturnedPlayer oyuncu)
		{
		    foreach (var steamOyuncu in Provider.clients)
		    {
		        var steamId = steamOyuncu.playerID.steamID;
				
		        if (Configuration.Instance.ÇıkışMesajlarıAktif)
                {
                    Color renk;
                    try
                    {
                        renk = oyuncu.Color;
                    }
                    catch
                    {
                        renk = Color.white;
                    }

                    EffectManager.sendUIEffect(Configuration.Instance.ÇıkışEfektiIdsi, 15963, steamId, true,
                        $"<color=#{Configuration.Instance.ÇıkışMesajıRengi}>{Translate("Çıkış", $"<color=#{ColorUtility.ToHtmlStringRGBA(renk)}>{oyuncu.CharacterName}</color>")}</color>");
                }

                if (Configuration.Instance.OyuncuSayısıAktif)
                {
                    EffectManager.sendUIEffect(Configuration.Instance.OyuncuSayısıEfektIdsi, 15964, steamId, true,
                        $"<color=#{Configuration.Instance.MevcutOyuncuSayısıRengi}>{Provider.clients.Count - 1}</color>/<color=#{Configuration.Instance.MaksimumOyuncuSayısıRengi}>{Provider.maxPlayers}</color>");
                }
            }
		}
		
		public override TranslationList DefaultTranslations => new TranslationList
	    {
	        { "Giriş", "{0} sunucuya bağlandı." },
	        { "Çıkış", "{0} sunucudan ayrıldı." }
	    };
	}
}