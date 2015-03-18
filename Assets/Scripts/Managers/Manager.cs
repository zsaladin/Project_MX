using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Manager : MonoBehaviour 
{
    static DataManager _data;
    public static DataManager Data 
    { 
        get 
        {
            if (_data == null)
            {
                _data = new DataManager();
                _data.Init();
            }

            return _data;
        }
    }

    public static ConstantManager Constant { get; private set; }
    public static EntityManager Entity { get; private set; }
    public static GameManager Game { get; private set; }
    public static EventManager Event { get; private set; }
    public static CoordinateManager Coordinate { get; private set; }

	void Awake () 
	{
        DontDestroyOnLoad(this);

        Init();
	}

    static void Init()
    {
        Constant = GameObject.FindObjectOfType<ConstantManager>();
        Constant.Init();

        Entity = GameObject.FindObjectOfType<EntityManager>();
        Entity.Init();

        Game = GameObject.FindObjectOfType<GameManager>();
        Game.Init();

        Event = GameObject.FindObjectOfType<EventManager>();
        Event.Init();

        Coordinate = GameObject.FindObjectOfType<CoordinateManager>();
        Coordinate.Init();
    }
}
