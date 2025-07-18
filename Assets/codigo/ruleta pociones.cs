using UnityEngine;

public class RuletaPocionesTiempo : MonoBehaviour
{
    public GameObject prefabTipo1;
    public GameObject prefabTipo2;

    public float radio = 10f;             // Radio del área circular
    public float alturaY = 1f;

    public float intervalo = 3f;
    public float tiempoDesaparicion = 5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= intervalo)
        {
            SpawnearUnaPocion();
            timer = 0f;
        }
    }

    void SpawnearUnaPocion()
    {
        // Genera una posición aleatoria dentro del círculo en XZ
        Vector2 offset2D = Random.insideUnitCircle * radio;
        Vector3 posicion = new Vector3(
            transform.position.x + offset2D.x,
            alturaY,
            transform.position.z + offset2D.y
        );

        GameObject prefabElegido = (Random.value > 0.5f) ? prefabTipo1 : prefabTipo2;

        GameObject pocion = Instantiate(prefabElegido, posicion, Quaternion.identity);

        Destroy(pocion, tiempoDesaparicion);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.3f, 1f, 0.3f, 0.2f); // Verde claro translúcido
        Vector3 centro = new Vector3(transform.position.x, alturaY, transform.position.z);
        Gizmos.DrawSphere(centro, 0.1f); // Marca el centro
        Gizmos.DrawWireSphere(centro, radio); // Dibuja el círculo de spawn
    }
}





