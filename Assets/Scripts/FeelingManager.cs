using System;
using UnityEngine;

namespace Librarian
{
    public enum Feeling
    {
        Fun,
        Calm,
        Fresh,
    }

    [Serializable]
    public struct Feelings
    {
        [Range(-100, 100)]
        public float Fun;

        [Range(-100, 100)]
        public float Calm;

        [Range(-100, 100)]
        public float Fresh;
    }

    public class FeelingManager
    {
        int FeelingsCount;

        private float[] _Feelings;
        private int[] _Order;
        private float[] _FeelingsSorted;
        private int[] _OrderSorted;
        private float[] _Rates;

        public FeelingManager(Feelings feelings)
        {
            FeelingsCount = Enum.GetNames(typeof(Feeling)).Length;

            _Feelings = new float[FeelingsCount];
            _Feelings[(int)Feeling.Fun] = feelings.Fun;
            _Feelings[(int)Feeling.Calm] = feelings.Calm;
            _Feelings[(int)Feeling.Fresh] = feelings.Fresh;

            _FeelingsSorted = new float[FeelingsCount];
            _Order = new int[FeelingsCount];
            for (int i = 0; i < FeelingsCount; i++)
            {
                _Order[i] = i;
            }
            _OrderSorted = new int[FeelingsCount];

            // TODO: this should be defined by character
            _Rates = new float[FeelingsCount];
            _Rates[(int)Feeling.Fun] = -1.0f;
            _Rates[(int)Feeling.Calm] = 0.1f;
            _Rates[(int)Feeling.Fresh] = -0.5f;
        }

        public void Update()
        {
            for (int i = 0; i < FeelingsCount; i++)
            {
                _Feelings[i] += _Rates[i] * Time.deltaTime;
                _Feelings[i] = Mathf.Clamp(_Feelings[i], -100, 100);
            }
        }

        public bool EvaluateFeelings(out Feeling result)
        {
            result = 0;
            
            if (FeelingsCount == 0) return false;

            if (FeelingsCount > 1)
            {
                Array.Copy(_Feelings, _FeelingsSorted, FeelingsCount);
                Array.Copy(_Order, _OrderSorted, FeelingsCount);

                Array.Sort(_FeelingsSorted, _OrderSorted);

                for (int i = 0; i < FeelingsCount; i++)
                {
                    int index = _OrderSorted[i];
                    
                    if (IsFeelingFelt(index))
                    {
                        result = (Feeling)index;
                        return true;
                    }
                }

                return false;
            }

            return IsFeelingFelt(0);
        }

        private bool IsFeelingFelt(int index)
        {
            float value = _Feelings[index];
            float random = UnityEngine.Random.Range(-100, 0);
            
            if (random > value) return true;

            return false;
        }

        public void UpdateFeeling(Feeling feeling, float value)
        {
            _Feelings[(int)feeling] += value;
        }

        public float GetFeeling(Feeling feeling)
        {
            return _Feelings[(int)feeling];
        }


    }

}
