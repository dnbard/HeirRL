using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeirRL.Characters
{
    public class CharacterAttribute 
    {
        public bool IsInflatedByLevel { get; set; }

        public int Rating { get; set; }
        public Character Parent { get; protected set; }

        public virtual float Value
        {
            get
            {
                return InflateByCharacterLevel(Rating);
            }
        }

        protected float InflateByCharacterLevel(float val)
        {
            if (IsInflatedByLevel)
                return val / Parent.Level;
            return val;
        }

        public CharacterAttribute(Character _parent, int AttributeRating = 0)
        {
            IsInflatedByLevel = false;
            Rating = AttributeRating;
            Parent = _parent;
        }        
    }

    public class DependentCharacterAttribute : CharacterAttribute
    {
        protected Dictionary<CharacterAttribute, float> Dependencies = new Dictionary<CharacterAttribute, float>();

        public override float Value
        {
            get
            {
                float result = 0;
                foreach (var dep in Dependencies)
                    result += dep.Key.Value * dep.Value;
                result += Rating;
                return InflateByCharacterLevel(result);
            }
        }

        public void AddDependency(CharacterAttribute attr, float mod)
        {
            if (Dependencies.ContainsKey(attr)) Dependencies[attr] = mod;
            else Dependencies.Add(attr, mod);

            if (OnDependecyChanged != null) OnDependecyChanged(this, null);
        }

        public void DeleteDependecy(CharacterAttribute attr)
        {
            if (Dependencies.ContainsKey(attr))
            {
                Dependencies.Remove(attr);
                if (OnDependecyChanged != null) OnDependecyChanged(this, null);
            }
        }

        public event EventHandler OnDependecyChanged;

        public DependentCharacterAttribute(Character _parent, int AttributeRating = 0)
            : base(_parent, AttributeRating)
        {
            IsInflatedByLevel = true;            
        }
    }

    sealed public class ForkedAttribute : DependentCharacterAttribute
    {
        public float MaxValueMod { get; set; }
        public float MinValueMod { get; set; }

        public ForkedAttribute(Character _parent, int AttributeRating)
            : base(_parent, AttributeRating)
        {
            MaxValueMod = 1;
            MinValueMod = 1;
        }

        public float MaxValue { get { return Value * MaxValueMod; } }
        public float MinValue { get { return Value * MinValueMod; } }

        public override string ToString()
        {
            float min = MinValue, max = MaxValue;
            if (min == max) return MaxValue.ToString();
            if (min > max)
            {
                var temp = min;
                min = max;
                max = temp;
            }

            return string.Format("{0}-{1}", min, max);
        }

        public float Get()
        {
            float rnd = (float)Program.Random.NextDouble();
            float min = MinValue, max = MaxValue;
            float diff = max - min;

            return min + diff * rnd;
        }
    }

    sealed public class ResourceAttribute : DependentCharacterAttribute
    {      
        private float _current;
        public float Current
        {
            get { return _current; }
            set
            {
                if (value < _current && OnAttributeDecrease != null)
                    OnAttributeDecrease(this, null);

                if (value > _current && OnAttributeIncrease != null)
                    OnAttributeIncrease(this, null);

                if (value <= 0)
                {
                    Current = 0;
                    if (OnZeroValue != null) OnZeroValue(Parent, null);                    
                }
                else if (value > Value) Current = Value;
            }
        }

        public ResourceAttribute(Character _parent, int AttributeRating)
            :base(_parent, AttributeRating)
        {
            Current = Value;
        }

        public event EventHandler OnZeroValue;
        public event EventHandler OnAttributeDecrease;
        public event EventHandler OnAttributeIncrease;
    }
}
