using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeirRL.Graphics;

namespace HeirRL.Characters
{
    public partial class Character : VisualComponent
    {
        public int Level { get; protected set; }

        public CharacterAttribute Might { get; protected  set; }
        public CharacterAttribute Endurance { get; protected set; }

        public ResourceAttribute Health { get; protected set; }

        public ForkedAttribute Damage { get; protected set; }

        protected virtual void InitializeAttributes()
        {
            Level = 1;

            Might = new CharacterAttribute(this, 10);
            Endurance = new CharacterAttribute(this, 10);

            Health = new ResourceAttribute(this, 10);
            Damage = new ForkedAttribute(this, 5);

            InitializeAttributesDependencies();
        }

        protected virtual void InitializeAttributesDependencies()
        {
            Health.OnDependecyChanged += (sender, args) => { Health.Current = Health.Value; };
            Health.OnZeroValue += (sender, args) => { (sender as Character).Remove(); };
            Health.AddDependency(Endurance, 4);

            Damage.AddDependency(Might, 2);
            Damage.AddDependency(Endurance, 0.5f);
            Damage.MinValueMod = 0.65f;
            Damage.MaxValueMod = 1.25f;
        }

        public void DoMeleeDamage(Character target)
        {
            if (target == null) return;

            var damage = new DamageHolder();
            damage.Add(DamageType.Physical, this.Damage.Get());

            target.Health.Current -= damage.GetAll();
        }
    }    
}
