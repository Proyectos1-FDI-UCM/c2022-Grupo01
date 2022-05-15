using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    #region references
    [SerializeField] private List<GameObject> roomPrefabs;
    [SerializeField] private GameObject objectRoomPrefab, Scenary, _firstRoom;
    [SerializeField] private GameObject[] bossRoomPrefab;
    #endregion

    #region properties
    private Vector3 instanceOffset = new Vector3(40,27,0);
    const int DIM = 16;
    const int MIN_SALAS = 7;
    const int MAX_SALAS = 10;
    const float ITERATIONS = DIM * DIM * 1.2f;
    private int numSalasCreadas, floorToGenerate = 0;

    private int[,] rooms;
    private GameObject[,] gameObjectPrefabs;

    private bool objectRoomGenerated = false;
    #endregion

    // Start is called before the first frame update
    public void GenerateFloor()
    {
        Destroy(Scenary);
        Scenary = null;
        int NUMSALAS = roomPrefabs.Count;
        Inicializa(out rooms, out gameObjectPrefabs);
        
        GeneraMatriz(rooms, ITERATIONS, NUMSALAS);
        ColocarSalas(rooms);
        AbrirPuertas(gameObjectPrefabs);
        floorToGenerate++;
		try
		{
            Inventory.Instance.activeItem.GetComponent<ActiveObject>().OnNewFloor();
		}
		catch
		{
            Debug.LogWarning("No active item to recharge");
		}
    }

    void Inicializa(out int[,] rooms, out GameObject[,] gameObjectPrefabs)
    {
        rooms = new int[DIM, DIM];
        gameObjectPrefabs = new GameObject[DIM, DIM];

        for(int i = 0; i < DIM; i++)
        {
            for(int j = 0; j < DIM; j++)
            {
                rooms[i, j] = -1;
                gameObjectPrefabs[i, j] = null;
            }
        }

        int h = Random.Range(0, DIM);
        int k = Random.Range(0, DIM);

        rooms[h, k] = 0;
    }

    void GeneraMatriz(int[,] rooms, float iterations, int numSalas)
    {
        int i = 0;
        numSalasCreadas = 0;
        while (numSalasCreadas < numSalas && MAX_SALAS > numSalasCreadas)
        {
            int j = Random.Range(0, DIM);
            int k = Random.Range(0, DIM);

            if (rooms[j, k] == -1 && ComprobarAdyacencia(rooms, j, k))
            {
                numSalasCreadas++;
                rooms[j, k] = numSalasCreadas;
            }
            i++;
        }
    }

    bool ComprobarAdyacencia(int[,] rooms, int i, int j)
    {
        bool adyacente = true;

        if (j == 0)
        {
            if (i == 0 && rooms[i, j + 1] == -1 && rooms[i + 1, j] == -1) adyacente = false;
            else if (i == DIM - 1 && rooms[i, j + 1] == -1 && rooms[i - 1, j] == -1) adyacente = false;
            else if (i != DIM - 1 && i != 0 && rooms[i, j + 1] == -1 && rooms[i + 1, j] == -1 && rooms[i - 1, j] == -1) adyacente = false;
        }
        else if (j == DIM - 1)
        {
            if (i == DIM - 1 && rooms[i, j - 1] == -1 && rooms[i - 1, j] == -1) adyacente = false;
            else if (i == 0 && rooms[i + 1, j] == -1 && rooms[i, j - 1] == -1) adyacente = false;
            else if (i != DIM - 1 && i != 0 && rooms[i + 1, j] == -1 && rooms[i - 1, j] == -1 && rooms[i, j - 1] == -1) adyacente = false;
        }
        else
        {
            if (i == DIM - 1 && rooms[i - 1, j] == -1 && rooms[i, j + 1] == -1 && rooms[i, j - 1] == -1) adyacente = false;
            else if (i == 0 && rooms[i + 1, j] == -1 && rooms[i, j + 1] == -1 && rooms[i, j - 1] == -1) adyacente = false;
            else if (i != DIM - 1 && i != 0 && rooms[i + 1, j] == -1 && rooms[i - 1, j] == -1 && rooms[i, j + 1] == -1 && rooms[i, j - 1] == -1) adyacente = false;
        }

        return adyacente;
    }

    void ColocarSalas(int[,] rooms)
    {
        GameObject newRoom = null;
        int[] lastRoomPosition = new int[2];
        int[] comprobarPosicion = new int[2];
        int rndObjectRoom = Random.Range(0, numSalasCreadas/2);

        bool tutorialRoomInsteadBossRoom = false;

        for(int i = 0; i < DIM; i++)
        {
            for(int j = 0; j < DIM; j++)
            {
                if(rooms[i,j] != -1 && roomPrefabs.Count > 0)
                {
                    if (newRoom == null)
                    {
                        if (rooms[i, j] == numSalasCreadas) numSalasCreadas -= 1; tutorialRoomInsteadBossRoom = true;
                        if (rooms[i, j] == rndObjectRoom && rndObjectRoom > 0) rndObjectRoom -= 1;
                        else if (rooms[i, j] == rndObjectRoom) rndObjectRoom += 1;

                        PonerPrimeraSala(ref lastRoomPosition, ref newRoom, i, j);
                    }
                    else if(rndObjectRoom == rooms[i, j])
                    {
                        PonerSala(ref comprobarPosicion, ref lastRoomPosition, ref newRoom, i, j, false, true);
                    }
                    else if (rooms[i,j] == numSalasCreadas)
                    {
                        if (tutorialRoomInsteadBossRoom) numSalasCreadas++;
                        PonerSala(ref comprobarPosicion, ref lastRoomPosition, ref newRoom, i, j, true, false);
                    }
                    else
                    {
                        PonerSala(ref comprobarPosicion, ref lastRoomPosition, ref newRoom, i, j, false, false);
                    }
                }
            }
        }
    }

    void PonerPrimeraSala(ref int[] lastRoomPosition, ref GameObject newRoom, int i, int j)
	{
        newRoom = Instantiate(_firstRoom, Vector3.zero, Quaternion.identity);

        try
        {
            newRoom.transform.parent = Scenary.transform;
        }
        catch
        {
            Scenary = new GameObject("---SCENARY---");
            newRoom.transform.parent = Scenary.transform;
        }

        PlayerManager.Instance.player.transform.position = newRoom.transform.position + new Vector3(-122, 45, 0);
        PlayerManager.Instance.UpdatePosition();
        PlayerManager.Instance.gancho.transform.position = PlayerManager.Instance._playerPosition;
        lastRoomPosition[0] = i;
        lastRoomPosition[1] = j;
        gameObjectPrefabs[lastRoomPosition[0], lastRoomPosition[1]] = newRoom;
    }

    void PonerSala(ref int[] comprobarPosicion, ref int[] lastRoomPosition, ref GameObject newRoom, int i, int j, bool bossRoom, bool objectRoom)
	{
        int rnd = Random.Range(0, roomPrefabs.Count);

        comprobarPosicion[0] = i - lastRoomPosition[0];
        comprobarPosicion[1] = j - lastRoomPosition[1];

        lastRoomPosition[0] = i;
        lastRoomPosition[1] = j;

        if(bossRoom) newRoom = Instantiate(bossRoomPrefab[floorToGenerate], newRoom.transform.position + new Vector3(comprobarPosicion[1] * instanceOffset.x, -comprobarPosicion[0] * instanceOffset.y, 0), Quaternion.identity);
        
        else if (objectRoom) newRoom = Instantiate(objectRoomPrefab, newRoom.transform.position + new Vector3(comprobarPosicion[1] * instanceOffset.x, -comprobarPosicion[0] * instanceOffset.y, 0), Quaternion.identity);
        
        else newRoom = Instantiate(roomPrefabs[rnd], newRoom.transform.position + new Vector3(comprobarPosicion[1] * instanceOffset.x, -comprobarPosicion[0] * instanceOffset.y, 0), Quaternion.identity);

		try
		{
			newRoom.transform.parent = Scenary.transform;
		}
		catch
		{
			Scenary = new GameObject("---SCENARY---");
			newRoom.transform.parent = Scenary.transform;
		}

		gameObjectPrefabs[lastRoomPosition[0], lastRoomPosition[1]] = newRoom;
        if(!objectRoom && !bossRoom) roomPrefabs.Remove(roomPrefabs[rnd]);
    }

    void AbrirPuertas(GameObject[,] rooms)
	{
        bool closeDoor = false;
        for(int i = 0; i < DIM; i++)
		{
            for(int j = 0; j < DIM; j++)
			{
                if (rooms[i, j] != null)
                {
                    DoorManager doorManager = rooms[i,j].GetComponent<DoorManager>();
                    if(i > 0 && rooms[i - 1,j] != null) { doorManager.Upper(closeDoor); }
                    if (i < DIM - 1 && rooms[i + 1, j] != null) doorManager.Lower(closeDoor);
                    if (j < DIM - 1 && rooms[i, j + 1] != null) doorManager.Right(closeDoor);
                    if (j > 0 && rooms[i, j - 1] != null) doorManager.Left(closeDoor);
                }
	        }
        }
    }
}