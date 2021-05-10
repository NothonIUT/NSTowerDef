using UnityEngine;

public class AddTouret : MonoBehaviour
{
    public GameObject spawnee;
    float radius;
    public GameObject circle;
    GameObject circledSpawn;

    private void Start()
    {
        // On initialise le cercle de taille de l'unit�
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        circledSpawn = Instantiate(circle);
        circledSpawn.SetActive(false);
        circledSpawn.transform.position = mousePos2D;
        SpriteRenderer sprite = circledSpawn.GetComponent<SpriteRenderer>();

        radius = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // On met � jour la position du cercle de s�l�ction
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        circledSpawn.transform.position = new Vector2(mousePos.x, mousePos.y);

        // On met � jour la couleur du cercle de s�l�ction
        if (circledSpawn.activeSelf)
        {
            UpdateSelection();
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Si le mode d'achat est actif, on essaye de spawn une tourelle
            if (circledSpawn.activeSelf) 
            {
                trySpawnTurret();
            } 

            // TEMP : On suppose qu'un clic gauche active le mode achat de tourelle
            else
            {
                turretChosen(spawnee);

                /**
                 * On devra utiliser la fonction turretChosen pour choisir une tourelle et passer en mode achat
                 * turretChosen(Turret);
                 */

            }         
        }

        // Clic droit d�sactive le mode achat
        else if(Input.GetMouseButtonDown(1) && circledSpawn.activeSelf)
        {
            circledSpawn.SetActive(false);
        }
    }

    /**
     * Mise � jour du cercle de selection
     * retourne si il est possible de placer la tourelle s�l�ctionn�e � l'emplacement de la souris
     */
    bool UpdateSelection()
    {
        // On r�cup�re la position de la souris
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // On v�rifie que rien n'emp�che de faire appara�tre une tourelle � cette endroit
        RaycastHit2D hit = Physics2D.CircleCast(mousePos2D, radius, Vector2.zero);

        SpriteRenderer sprite = circledSpawn.GetComponent<SpriteRenderer>();
        if (hit.collider != null)
        {
            // On affiche le cercle en rouge si impossible de poser une tourelle � cette emplacement
            sprite.color = new Color(1, 0, 0, 0.25f);
            return false;
        }
        else
        {
            // On affiche le cercle en vert si il est possible de poser une tourelle � cette emplacement
            sprite.color = new Color(0, 1, 0, 0.25f);
            return true;
        }
    }


    /**
     * Essai d'apparition de la tourelle s�l�ctionner
     */
    void trySpawnTurret()
    {
        // On r�cup�re la position de la souris
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // On v�rifie que rien n'emp�che de faire appara�tre une tourelle � cette endroit
        RaycastHit2D hit = Physics2D.CircleCast(mousePos2D, radius, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Un objet emp�che le positionnement de la tourelle" + hit.collider.gameObject.name);

            // TEMP : On supprime la tourelle en lui cliquant dessus
            if (hit.collider.gameObject.name == "Turret(Clone)")
            {
                Destroy(hit.collider.gameObject);
            }
        }

        else
        {
            // On fait appara�tre la tourelle s�l�ctionn�e
            GameObject spawned = Instantiate(spawnee);
            Debug.Log("Tourelle apparu " + spawned.gameObject.name);
            spawned.transform.position = mousePos2D;

            // On sort du mode achat
            circledSpawn.SetActive(false);
        }
    }

    void turretChosen( GameObject turret)
    {
        spawnee =turret;
        //radius = turret.radius;

        circledSpawn.transform.localScale = new Vector3(1,1,1) * radius * 2; 

        circledSpawn.SetActive(true);

    }

}
        