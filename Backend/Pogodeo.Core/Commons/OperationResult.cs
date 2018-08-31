using System.Collections.Generic;
using System.Text;

namespace Pogodeo.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class OperationResult
    {
        #region Public Properties

        public List<Error> Errors { get; set; }

        public bool Success { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public OperationResult()
        {
            Success = false;
            Errors = new List<Error>();
        } 

        public OperationResult(bool success)
        {
            this.Success = success;
        }

        public OperationResult(string error)
        {
            this.Success = false;
            this.Errors = new List<Error>()
            {
                new Error()
                {
                    Message = error,
                }
            };
        }

        public OperationResult(string error, string classification = null, string identyfication = null)
        {
            this.Success = false;
            this.Errors = new List<Error>()
            {
                new Error()
                {
                    Message = error,
                    Classification = classification,
                    Identification = identyfication,
                }
            };
        }

        #endregion

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var error in Errors)
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }
                result.Append(error.Message);
            }
            return result.ToString();
        }

        public void Merge(OperationResult another)
        {
            this.Success &= another.Success;
            this.Errors.AddRange(another.Errors);
        }

        public OperationResult Error(string message, string identification = null, string classification = null)
        {
            Errors.Add(new Error()
            {
                Message = message,
                Identification = identification,
                Classification = classification,
            });

            return this;
        }
    }

    public class OperationResult<TResult> : OperationResult
    {
        public TResult Result { get; set; }

        public OperationResult() : base()
        { }

        public OperationResult(OperationResult operation, TResult result)
            : this(operation.Success, result)
        {
            Errors = operation.Errors;
        }

        public OperationResult(bool success)
            : base(success)
        { }

        public OperationResult(bool success, TResult result)
            : base (success)
        {
            Result = result;
        }

        public OperationResult(string error)
            : base(false)
        {
            Errors = new List<Error>()
            {
                new Error()
                {
                    Message = error,
                }
            };
        }

        public OperationResult(string error, string classification = null, string identyfication = null)
            : base(false)
        {
            Errors = new List<Error>()
            {
                new Error()
                {
                    Classification = classification,
                    Message = error,
                    Identification = identyfication,
                }
            };
        }

        public OperationResult<TResult> WithResult(TResult result)
        {
            Result = result;
            return this;
        }
    }
}
