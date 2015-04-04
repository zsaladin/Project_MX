using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
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
        public static UIManager UI { get; private set; }
        public static EffectManager Effect { get; private set; }

        void Awake()
        {
            DontDestroyOnLoad(this);

            Init();
        }

        static void Init()
        {
            Constant = GameObject.FindObjectOfType<ConstantManager>();
            Constant.Init();

            UI = GameObject.FindObjectOfType<UIManager>();
            UI.Init();

            Coordinate = GameObject.FindObjectOfType<CoordinateManager>();
            Coordinate.Init();

            Entity = GameObject.FindObjectOfType<EntityManager>();
            Entity.Init();

            Game = GameObject.FindObjectOfType<GameManager>();
            Game.Init();

            Event = GameObject.FindObjectOfType<EventManager>();
            Event.Init();

            Effect = GameObject.FindObjectOfType<EffectManager>();
            Effect.Init();
        }
    }
}