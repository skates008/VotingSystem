
using System;


namespace VotingSiteAPI.Services.Exceptions
{
    public class VoterNotFoundException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Default constructor
        /// </summary>
        public VoterNotFoundException()
        { }

        /// <inheritdoc />
        /// <summary>
        /// Constructor accepting a single string message 
        /// </summary>
        /// <param name="message"></param>
        /// 
        public VoterNotFoundException(string message) : base(message)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Constructor accepting a string message and an inner exception 
        /// which will be wrapped by this custom exception class.
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="oExInner"></param>
        public VoterNotFoundException(string strMsg, Exception oExInner) : base(strMsg, oExInner)
        { }

        /////// <summary>
        /////// make our own exception message. Should be able to construct 
        /////// with the name of the window we couldn't find, then get this 
        /////// as the .Message which will include the name of the unfound window.
        /////// </summary>
        ////public override string Message
        ////{
        ////    get
        ////    {
        ////        string errorMessage;
        ////        errorMessage = String.Format(
        ////            "Unable to find window \"{0}\".", base.Message);

        ////        return errorMessage;
        ////    }
        ////}

    }

}
