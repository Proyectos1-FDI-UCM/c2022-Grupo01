using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenu : MonoBehaviour
{
    public static void LoadJuan()
    {
        SceneManager.LoadSceneAsync("JuanScene");
    }

    public static void LoadBossEsponja()
    {
        SceneManager.LoadSceneAsync("SpongeScene");
    }

    public static void LoadBayeta()
    {
        SceneManager.LoadSceneAsync("BayetaScene");
    }

    public static void LoadBarrita()
    {
        SceneManager.LoadSceneAsync("BarritaEnergeticaScene");
    }

    public static void LoadBebida()
    {
        SceneManager.LoadSceneAsync("BebidaEnergeticaScene");
    }

    public static void LoadFlotador()
    {
        SceneManager.LoadSceneAsync("HolyFlotadorScene");
    }

    public static void LoadGancho()
    {
        SceneManager.LoadSceneAsync("HookScene");
    }

    public static void LoadHielo()
    {
        SceneManager.LoadSceneAsync("BolsaDeHieloScene");
    }

    public static void LoadRuedo()
    {
        SceneManager.LoadSceneAsync("RollScene");
    }

    public static void LoadChaleco()
    {
        SceneManager.LoadSceneAsync("ChalecoSalvavidasScene");
    }

    public static void LoadCabezaJuan()
    {
        SceneManager.LoadSceneAsync("CabezaDeJuanScene");
    }

    public static void Return()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
