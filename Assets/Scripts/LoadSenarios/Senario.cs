using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.LoadSenarios
{
    public class Senario
    {
        public string Name { get; }
        public SenarioType Type { get; }
        public float FallingSpeed { get; }
        public int digits { get; }

        public int LifeNumber { get; }
        public int NumberOfDeleteOnDecreaseLife { get; }
        public float RangeOfDeleteOnDecreaseLife { get; }

        private float UserTypingTime;

        public List<NumberSpawnDelayTimeInstraction> NumberSpawnDelayTimeInstractions { get; }

        public Senario(string _name,SenarioType _type, float _FallingSpeed, List<NumberSpawnDelayTimeInstraction> _numberSpawnDelayTimeInstractions, int _digits, int lifeNumber, int numberOfDeleteOnDecreaseLife, float rangeOfDeleteOnDecreaseLife)
        {
            this.Name = _name;
            this.Type = _type;
            this.FallingSpeed = _FallingSpeed;
            this.NumberSpawnDelayTimeInstractions = _numberSpawnDelayTimeInstractions;
            this.digits = _digits;
            LifeNumber = lifeNumber;
            NumberOfDeleteOnDecreaseLife = numberOfDeleteOnDecreaseLife;
            RangeOfDeleteOnDecreaseLife = rangeOfDeleteOnDecreaseLife;
        }

        public void SetUserTypingTime( float _userTypingTime )
        {
            UserTypingTime = _userTypingTime;
        }

        /// <summary>
        /// DelayTimeの変化率の計算
        /// 終了時は-1000fを返す
        /// </summary>
        /// <param name="elapsedTime">現在の経過時間</param>
        /// <returns>時間の変化量</returns>
        public float GetSpawnDelayTimeByTime(float elapsedTime)
        {
            if (elapsedTime == 0f)
            {
                if(this.Type == SenarioType.Auto)
                {
                    return NumberSpawnDelayTimeInstractions[0].NumberSpawnDelayTime * UserTypingTime;
                }
                return NumberSpawnDelayTimeInstractions[0].NumberSpawnDelayTime;
            }

            NumberSpawnDelayTimeInstraction currentInstrantion = NumberSpawnDelayTimeInstractions.FindLast(i => i.ChangeAt < elapsedTime);
            

            if (NumberSpawnDelayTimeInstractions.IndexOf(currentInstrantion) + 1 == NumberSpawnDelayTimeInstractions.Count) return -1000f;


            NumberSpawnDelayTimeInstraction nextInstraction = NumberSpawnDelayTimeInstractions[ NumberSpawnDelayTimeInstractions.IndexOf(currentInstrantion) +1];
           
            float dt = nextInstraction.ChangeAt - currentInstrantion.ChangeAt;
            float ds = 0;
            float previousSpeed = 0;
            if( this.Type == SenarioType.Auto)
            {
                ds = nextInstraction.NumberSpawnDelayTime * UserTypingTime  - currentInstrantion.NumberSpawnDelayTime * UserTypingTime;
                previousSpeed = currentInstrantion.NumberSpawnDelayTime * UserTypingTime;
            }
            else
            {
                ds = nextInstraction.NumberSpawnDelayTime - currentInstrantion.NumberSpawnDelayTime;
                previousSpeed = currentInstrantion.NumberSpawnDelayTime;
            }
            
            
            return previousSpeed + ds*(elapsedTime - currentInstrantion.ChangeAt)/dt;
        }
    }

    public class NumberSpawnDelayTimeInstraction
    {
        public float NumberSpawnDelayTime { get; }
        public float ChangeAt { get; }

        public NumberSpawnDelayTimeInstraction(float numberSpawnDelayTIme, float changeAt)
        {
            NumberSpawnDelayTime = numberSpawnDelayTIme;
            ChangeAt = changeAt;
        }
    }
    public enum SenarioType
    {
        Training,
        Measure,
        Auto,
        Manual,
        Task
    }
}
