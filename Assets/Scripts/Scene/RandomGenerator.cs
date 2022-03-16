using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    #region references
    [SerializeField] private List<GameObject> roomPrefabs;
    #endregion

    #region properties
    private Vector3 instanceOffset = new Vector3(40,27,0);
    const int DIM = 16;
    const int MIN_SALAS = 7;
    const float ITERATIONS = DIM * DIM * 1.2f;

    private int[,] rooms;
    private GameObject[,] gameObjectPrefabs;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        int NUMSALAS = roomPrefabs.Count;
        Inicializa(out rooms, out gameObjectPrefabs);
        
        GeneraSala(rooms, ITERATIONS, NUMSALAS);
        ColocarSalas(rooms, gameObjectPrefabs);
        AbrirPuertas(gameObjectPrefabs);
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
        int numSalasCreadas = 1;
        while (i < iterations && numSalasCreadas < numSalas || numSalasCreadas < MIN_SALAS)
        {
            int j = Random.Range(0, DIM);
            int k = Random.Range(0, DIM);

            if (rooms[j, k] == -1 && ComprobarAdyacencia(rooms, j, k))
            {
                rooms[j, k] = numSalasCreadas;
                numSalasCreadas++;
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

        for(int i = 0; i < DIM; i++)
        {
            for(int j = 0; j < DIM; j++)
            {
                if(rooms[i,j] != -1 && roomPrefabs.Count > 0)
                {
                    if (newRoom == null)
                    {
                        int rnd = Random.Range(0, roomPrefabs.Count);
                        newRoom = Instantiate(roomPrefabs[rnd], Vector3.zero, Quaternion.identity);
                        gameObjectPrefabs[i, j] = newRoom;
                        roomPrefabs.Remove(roomPrefabs[rnd]);
                        PlayerManager.Instance.player.transform.position = newRoom.transform.position + new Vector3(-122,45,0);
                        lastRoomPosition[0] = i;
                        lastRoomPosition[1] = j;
                    }

                    else
                    {
                        int rnd = Random.Range(0, roomPrefabs.Count);
                        int[] comprobarPosicion = new int[2];

                        comprobarPosicion[0] = i - lastRoomPosition[0];
                        comprobarPosicion[1] = j - lastRoomPosition[1];

                        lastRoomPosition[0] = i;
                        lastRoomPosition[1] = j;

                        newRoom = Instantiate(roomPrefabs[rnd], newRoom.transform.position + new Vector3(comprobarPosicion[1]*instanceOffset.x,-comprobarPosicion[0]*instanceOffset.y,0), Quaternion.identity);
                        gameObjectPrefabs[i, j] = newRoom;
                        roomPrefabs.Remove(roomPrefabs[rnd]);
                    }
                }
            }
        }
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