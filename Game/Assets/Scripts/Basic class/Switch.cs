using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Switch : MonoBehaviour
{
    public GameObject activeFraem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Character>();
        if (player != null)
        {
            activeFraem.SetActive(true);
            LoadMonsters();
            //var monsters = activeFraem.GetComponentsInChildren<Transform>();
            //Debug.Log(new Monster() is MonoBehaviour);
            //monsters.Where(x => x.gameObject.tag == "")
            //    .Select
            //Debug.Log(monsters.Length);
            //foreach (var monster in monsters) monster.DoRewind();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Character>();
        if (player != null)
        {
            activeFraem.SetActive(false);
            var forms = activeFraem.GetComponentsInChildren<Transform>();
            var monsters = forms.Where(x => x.gameObject.tag == "Respawn");
            foreach (var monster in monsters) Destroy(monster.gameObject);
            //var monsters = activeFraem.GetComponentsInChildren<Monster>();
            //foreach (var i in monsters) Destroy(i.gameObject);
        }
    }

    public virtual void LoadMonsters()
    {

    }

    public void AddMonster(MonoBehaviour monsterRef, float x, float y, int sign = 1)
    {
        var x1 = x + activeFraem.transform.position.x;
        var y1 = y + activeFraem.transform.position.y;
        var monster = Instantiate(monsterRef, new Vector2(x1, y1), new Quaternion(), activeFraem.transform);
        monster.transform.localScale *= new Vector2(sign, 1);
    }
}
