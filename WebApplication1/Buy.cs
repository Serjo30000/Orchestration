using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication21.Models;

namespace WebApplication1
{
    public enum Transition
    {
        VERIFYCONSUMER, VERIFICATIONFAILED, CONSUMERVERIFIRED, PURCHASECREATIONFAILED,AUTHORIZECARD, CARDAUTHORIZATIONFAILED, CARDAUTHORIZATIONVERIFY,ORDERCONFIRMED,PURCHASE
    }
    internal abstract class State
    {
        internal virtual void HandleMark(Buy buy, Transition transition)
        {
            ChangeState(buy, transition);
        }
        protected abstract void ChangeState(Buy buy, Transition transition);
    }
    internal class ConsumerState : State
    {
        internal ConsumerState()
        {
            Debug.WriteLine("Объект в ожидании:");
            Hope();
        }
        protected override void ChangeState(Buy buy, Transition transition)
        {
            switch (transition)
            {
                case Transition.VERIFYCONSUMER:
                    {
                        buy.State = new ConsumerState(); // S1
                        break;
                    }
                case Transition.VERIFICATIONFAILED:
                    {
                        buy.State = new PurchaseRejectionState(); // S3
                        break;
                    }
                case Transition.CONSUMERVERIFIRED:
                    {
                        buy.State = new CreatingApplicationState(); // S3
                        break;
                    }
            }
        }
        private void Hope() // y3
        {
            Debug.WriteLine("Объект ожидает изменений.");
        }
    }
    // Состояние жалости (S1)
    internal class PurchaseRejectionState : State
    {
        internal PurchaseRejectionState()
        {
            Debug.WriteLine("Покупка в состоянии отклонения:");
            Calm();
        }
        protected override void ChangeState(Buy buy, Transition transition)
        {
            switch (transition)
            {
                case Transition.VERIFICATIONFAILED:
                    {
                        buy.State = new PurchaseRejectionState(); // S4
                        break;
                    }
                case Transition.PURCHASECREATIONFAILED:
                    {
                        buy.State = new PurchaseDeclinedState(); // S0
                        break;
                    }
            }
        }
        private void Calm() // y2
        {
            Debug.WriteLine("Покупка отклонена.");
        }
    }
    // Состояние сильного гнева (S2)
    internal class CreatingApplicationState : State
    {
        internal CreatingApplicationState()
        {
            Debug.WriteLine("Заявка в состоянии создания:");
            BeatBelt();
        }
        protected override void ChangeState(Buy buy, Transition transition)
        {
            switch (transition)
            {
                case Transition.CONSUMERVERIFIRED:
                    {
                        buy.State = new CreatingApplicationState(); // S2
                        break;
                    }
                case Transition.AUTHORIZECARD:
                    {
                        buy.State = new AuthorizationState(); // S0
                        break;
                    }
                case Transition.PURCHASECREATIONFAILED:
                    {
                        buy.State = new PurchaseDeclinedState(); // S0
                        break;
                    }
            }
        }

        private void BeatBelt() // y0
        {
            Debug.WriteLine("Создание заявки.");
        }
    }
    // Состояние радости (S3)
    internal class PurchaseDeclinedState : State
    {
        internal PurchaseDeclinedState()
        {
            Debug.WriteLine("Покупка в состоянии отклонения по причине ошибки:");
            Joy();
        }
        protected override void ChangeState(Buy buy, Transition transition)
        {
            switch (transition)
            {
                case Transition.PURCHASECREATIONFAILED:
                    {
                        buy.State = new PurchaseDeclinedState(); // S1
                        break;
                    }
            }
        }

        private void Joy() // y4
        {
            Debug.WriteLine("Покупка отклонена и процесс прекращён.");
        }
    }
    // Состояние гнева (S4)
    internal class AuthorizationState : State
    {
        internal AuthorizationState()
        {
            Debug.WriteLine("Карта в состоянии авторизации:");
            Scold();
        }
        protected override void ChangeState(Buy buy, Transition transition)
        {
            switch (transition)
            {
                case Transition.AUTHORIZECARD:
                    {
                        buy.State = new AuthorizationState(); // S2
                        break;
                    }
                case Transition.CARDAUTHORIZATIONFAILED:
                    {
                        buy.State = new ApplicationRejectState(); // S0
                        break;
                    }
                case Transition.CARDAUTHORIZATIONVERIFY:
                    {
                        buy.State = new ConfirmationApplicationState(); // S0
                        break;
                    }
            }
        }

        private void Scold() // y1
        {
            Debug.WriteLine("Карта авторизуется.");
        }
    }
    // Состояние сильной радости (S5)
    internal class ApplicationRejectState : State
    {
        internal ApplicationRejectState()
        {
            Debug.WriteLine("Заявка в состоянии отклонения:");
            Exult();
        }
        protected override void ChangeState(Buy buy, Transition transition)
        {
            switch (transition)
            {
                case Transition.CARDAUTHORIZATIONFAILED:
                    {
                        buy.State = new ApplicationRejectState(); // S1
                        break;
                }
                case Transition.VERIFICATIONFAILED:
                    {
                        buy.State = new PurchaseRejectionState(); // S5
                        break;
                    }
            }
        }

        private void Exult() // y5
        {
            Debug.WriteLine("Заявка отклонена.");
        }
    }
    internal class ConfirmationApplicationState : State
    {
        internal ConfirmationApplicationState()
        {
            Debug.WriteLine("Заявка в состоянии подтверждения:");
            Exult();
        }
        protected override void ChangeState(Buy buy, Transition transition)
        {
            switch (transition)
            {
                case Transition.CARDAUTHORIZATIONVERIFY:
                    {
                        buy.State = new ConfirmationApplicationState(); // S1
                        break;
                    }
                case Transition.ORDERCONFIRMED:
                    {
                        buy.State = new PurchaseConfirmedState(); // S5
                        break;
                    }
            }
        }

        private void Exult() // y5
        {
            Debug.WriteLine("Заявка подтверждена.");
        }
    }
    internal class PurchaseConfirmedState : State
    {
        internal PurchaseConfirmedState()
        {
            Debug.WriteLine("Покупка в состоянии подтверждения:");
            Exult();
        }
        protected override void ChangeState(Buy buy, Transition transition)
        {
            switch (transition)
            {
                case Transition.ORDERCONFIRMED:
                    {
                        buy.State = new PurchaseConfirmedState(); // S1
                        break;
                    }
                case Transition.PURCHASE:
                    {
                        buy.State = new PurchaseState(); // S5
                        break;
                    }
            }
        }

        private void Exult() // y5
        {
            Debug.WriteLine("Покупка подтверждена.");
        }
    }
    internal class PurchaseState : State
    {
        internal PurchaseState()
        {
            Debug.WriteLine("Покупка в состоянии завершения:");
            Exult();
        }
        protected override void ChangeState(Buy buy, Transition transition)
        {
            switch (transition)
            {
                case Transition.PURCHASE:
                    {
                        buy.State = new PurchaseState(); // S1
                        break;
                    }
            }
        }

        private void Exult() // y5
        {
            Debug.WriteLine("Покупка завершена.");
        }
    }

    public class Buy
    {
        internal State State { get; set; }
        public Buy()
        {
            State = new ConsumerState();
        }
        public void FindOut(Transition transition)
        {
            State.HandleMark(this, transition);
        }
    }
}
