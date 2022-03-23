using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomGenerator : MonoBehaviour
{
    #region references
    [SerializeField] private List<GameObject> roomPrefabs;
    [SerializeField] private GameObject objectRoomPrefab;
    [SerializeField] private GameObject[] bossRoomPrefab;
    [SerializeField] private NavMeshSurface2d navMeshBaker;
    #endregion

    #region properties
    private Vector3 instanceOffset = new Vector3(40,27,0);
    const int DIM = 16;
    const int MIN_SALAS = 7;
    const float ITERATIONS = DIM * DIM * 1.2f;
    private int numSalasCreadas, floorToGenerate = 0;

    private int[,] rooms;
    private GameObject[,] gameObjectPrefabs;

    private bool objectRoomGenerated = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        int NUMSALAS = roomPrefabs.Count;
        Inicializa(out rooms, out gameObjectPrefabs);
        
        GeneraSala(rooms, ITERATIONS, NUMSALAS);
        ColocarSalas(rooms, gameObjectPrefabs);
        AbrirPuertas(gameObjectPrefabs);
        //navMeshBaker.BuildNavMesh();
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

    void GeneraSala(int[,] rooms, float iterations, int numSalas)
    {
        int i = 0;
        numSalasCreadas = 0;
        while (numSalasCreadas < numSalas)
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

    void ColocarSalas(int[,] rooms, GameObject[,] gameObjectPrefabs)
    {
        GameObject newRoom = null;
        int[] lastRoomPosition = new int[2];
        int[] comprobarPosicion = new int[2];
        int rndObjectRoom = Random.Range(0, numSalasCreadas/2);

        Debug.Log(rndObjectRoom);

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
                        int rnd = Random.Range(0, roomPrefabs.Count);

                        //Prueba para que el player no explote
                        newRoom = Instantiate(roomPrefabs[0], Vector3.zero, Quaternion.identity);
                        roomPrefabs.Remove(roomPrefabs[0]);
                        PlayerManager.Instance.player.transform.position = newRoom.transform.position + new Vector3(-122,45,0);
                        lastRoomPosition[0] = i;
                        lastRoomPosition[1] = j;
                        gameObjectPrefabs[lastRoomPosition[0], lastRoomPosition[1]] = newRoom;
                    }
                    else if(!objectRoomGenerated && rndObjectRoom == rooms[i, j])
                    {
                        comprobarPosicion[0] = i - lastRoomPosition[0];
                        comprobarPosicion[1] = j - lastRoomPosition[1];
                        newRoom = Instantiate(objectRoomPrefab, newRoom.transform.position + new Vector3(comprobarPosicion[1] * instanceOffset.x, -comprobarPosicion[0] * instanceOffset.y, 0), Quaternion.identity);
                        lastRoomPosition[0] = i;
                        lastRoomPosition[1] = j;
                        gameObjectPrefabs[lastRoomPosition[0], lastRoomPosition[1]] = newRoom;
                        objectRoomGenerated = true;
                    }
                    else if (rooms[i,j] == numSalasCreadas)
                    {
                        if (tutorialRoomInsteadBossRoom) numSalasCreadas++;
                        comprobarPosicion[0] = i - lastRoomPosition[0];
                        comprobarPosicion[1] = j - lastRoomPosition[1];
                        newRoom = Instantiate(bossRoomPrefab[floorToGenerate], newRoom.transform.position + new Vector3(comprobarPosicion[1] * instanceOffset.x, -comprobarPosicion[0] * instanceOffset.y, 0), Quaternion.identity);
                        lastRoomPosition[0] = i;
                        lastRoomPosition[1] = j;
                        gameObjectPrefabs[lastRoomPosition[0], lastRoomPosition[1]] = newRoom;
                    }
                    else
                    {
                        int rnd = Random.Range(0, roomPrefabs.Count);

                        comprobarPosicion[0] = i - lastRoomPosition[0];
                        comprobarPosicion[1] = j - lastRoomPosition[1];

                        lastRoomPosition[0] = i;
                        lastRoomPosition[1] = j;
                        newRoom = Instantiate(roomPrefabs[rnd], newRoom.transform.position + new Vector3(comprobarPosicion[1] * instanceOffset.x, -comprobarPosicion[0] * instanceOffset.y, 0), Quaternion.identity);
                        gameObjectPrefabs[lastRoomPosition[0], lastRoomPosition[1]] = newRoom;
                        roomPrefabs.Remove(roomPrefabs[rnd]);
                    }
                }
            }
        }
        floorToGenerate++;
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