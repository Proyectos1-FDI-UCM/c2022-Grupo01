using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    #region references
    [SerializeField] private List<GameObject> roomPrefabs;
    [SerializeField] private Vector3[] instanceOffset;
    #endregion

    #region properties
    const int DIM = 16;
    const int NUMSALAS = 10;
    const float ITERATIONS = DIM * DIM * 1.2f;

    private int[,] rooms;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Inicializa(out rooms);

        GeneraSala(rooms, ITERATIONS, NUMSALAS);
        ColocarSalas(rooms);
    }

    void Inicializa(out int[,] rooms)
    {
        rooms = new int[DIM, DIM];

        for(int i = 0; i < DIM; i++)
        {
            for(int j = 0; j < DIM; j++)
            {
                rooms[i, j] = -1;
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
        while (i < iterations && numSalasCreadas < numSalas)
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

    void ColocarSalas(int[,] rooms)
    {
        bool firstRoom = false;
        GameObject newRoom = null;
        for(int i = 0; i < DIM; i++)
        {
            for(int j = 0; j < DIM; j++)
            {
                if(rooms[i,j] != -1 && roomPrefabs.Count > 0)
                {
                    if (!firstRoom)
                    {
                        int rnd = Random.Range(0, roomPrefabs.Count);
                        newRoom = Instantiate(roomPrefabs[rnd], Vector3.zero, Quaternion.identity);
                        roomPrefabs.Remove(roomPrefabs[rnd]);
                        firstRoom = true;
                        PlayerManager.Instance.player.transform.position = newRoom.transform.position + new Vector3(-122,45,0);
                    }

                    else
                    {
                        int rnd = Random.Range(0, roomPrefabs.Count);
                        int otherRnd = Random.Range(0, 4);
                        newRoom = Instantiate(roomPrefabs[rnd], newRoom.transform.position + instanceOffset[otherRnd], Quaternion.identity);
                        roomPrefabs.Remove(roomPrefabs[rnd]);
                    }

                }
            }
        }
    }
}
