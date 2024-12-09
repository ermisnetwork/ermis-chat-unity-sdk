using Ermis.Core.Models;

namespace ErmisChat.Samples
{
    internal class CodeMigrationTo5x
    {
        internal void VariableDeclarationAndAssignment()
        {
            // Old (enum)
            //ErmisMessageType messageType = ErmisMessageType.Regular;

            // New (struct)
            ErmisMessageType messageType = ErmisMessageType.Regular;
        }
        
        internal void MethodParametersAndAssignment()
        {
      
        }
        
        // New (struct)
        public void ProcessMessage(ErmisMessageType type)
        {
            
        }

        internal void Equality()
        {
            ErmisMessageType messageType = ErmisMessageType.Regular;
            
            // Old (enum)
            if (messageType == ErmisMessageType.Regular)
            {
                // Handle regular message
            }

            // New (struct)
            if (messageType == ErmisMessageType.Regular)
            {
                // Handle system message
            }
        }

        internal void SwitchStatements()
        {
            

            ErmisMessageType messageType = ErmisMessageType.Regular;
            
            // New (struct) - Using if-else statements
            if (messageType == ErmisMessageType.Regular)
            {
                // Handle regular message
            }
            else if (messageType == ErmisMessageType.System)
            {
                // Handle system message
            }
            
            switch (messageType)
            {
                case var type when type == ErmisMessageType.Regular:
                    // Handle regular message
                    break;
                case var type when type == ErmisMessageType.System:
                    // Handle system message
                    break;
                // ...
            }
        }

        internal void ExplicitInitialization()
        {
            ErmisMessageType messageType = ErmisMessageType.Regular;
        }
    
        // Const-assignment alternative
        public static readonly ErmisMessageType DefaultMessageType = ErmisMessageType.Regular;
    }
}