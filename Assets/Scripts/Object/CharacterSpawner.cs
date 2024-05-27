using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Node node;
    Node spawnNode;
    List<Character> characters;

    public void Allocate(Node allocatedNode, params Color[] colors)
    {
        Character character;

        characters = new List<Character>();
        spawnNode = allocatedNode;

        foreach (Color color in colors)
        {
            character = ObjectManager.Instance.AllocateObject<Character>(spawnNode, color, false);
            characters.Add(character);
        }
    }

    public void Spawn()
    {
        Character character;

        if (!spawnNode.walkable || characters.Count == 0) return;


        character = characters[Random.Range(0, characters.Count)];
        characters.Remove(character);
        character.gameObject.SetActive(true);
        character.node.walkable = false;
    }
}
