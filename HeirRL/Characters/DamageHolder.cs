using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeirRL.Characters
{
    public class DamageHolder: Dictionary<DamageType, float>
    {
        public new void Add(DamageType type, float ammount)
        {
            if (ammount <= 0) return;
            if (ContainsKey(type)) this[type] += ammount;
            else base.Add(type, ammount);
        }

        public void Subtract(DamageType type, float ammount)
        {
            if (ammount <= 0) return;

            if (ContainsKey(type))
            {
                if (this[type] - ammount > 0)
                    this[type] -= ammount;
                else this.Remove(type);
            }
        }

        public float Get(DamageType type)
        {
            if (ContainsKey(type)) return this[type];
            return 0;
        }

        public float GetAll()
        {
            float result = 0;
            foreach (var damage in this)            
                result += damage.Value;
            return result;
        }
    }
}
