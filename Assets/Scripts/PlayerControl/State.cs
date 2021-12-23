using UnityEngine;

namespace Game.PlayerControl
{
    /* ZTK was here
     * Çok güzel state machine sistemi.
     * Bu tarz yaklaşımın sıkıntıları;
     * 
     * Her yeni state için yeni class oluşturulması gerekiyor
     * Her state bir class olduğu için erişim sıkıntısı çekilebilir
     * Bu koşula özel bir state machine yazılması şart oluyor, yani başka projede kullanılması zorlaşıyor.
     * 
     * On StateEnter, Exit ve Update methodları tanımlanması yerine bunlar bir delegate şeklinde tutulsa.
     * Bu state machine i kullanmak isteyen class kendi içerisindeki methodlardan buraya delegate olarak yönlendirse.
     * Hem farklı classlarda kullanılabilir,
     * Hem her class kendi içindeki bir method u gönderdiği için class verilerine erişim sıkıntısı çekmez,
     * Hem de state machine sistemin oyun kodlarından tamamen habersiz olacağı için farklı projelerde rahatlıkla kullanılabilir olur.
     */

    
    public abstract class State
    {
        protected PlayerController player;
        protected Animator animator;

        public abstract void OnUpdate();

        public virtual void OnStateEnter() { }
        public virtual void OnStateExit() { }

        public State(PlayerController player)
        {
            this.player = player;
            animator = player.animator;
        }
    }

    public class IdleState : State
    {
        public IdleState(PlayerController player) : base(player)
        {

        }

        public override void OnUpdate()
        {

        }
    }

    public class RunState : State
    {
        public RunState(PlayerController player) : base(player)
        {

        }

        public override void OnStateEnter()
        {
            animator.SetTrigger("Run");
        }

        public override void OnUpdate()
        {
            player.Move();
        }
    }

    public class DanceState : State
    {
        public DanceState(PlayerController player) : base(player)
        {

        }

        public override void OnStateEnter()
        {
            animator.SetTrigger("Dance");
        }

        public override void OnUpdate()
        {

        }
    }
    

}
