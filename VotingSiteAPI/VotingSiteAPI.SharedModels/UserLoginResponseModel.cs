

namespace VotingSiteAPI.SharedModels
{
    public class UserLoginResponseModel
    {
        /// <summary>
        /// Gets or sets a boolean value indicating whether (true) or not
        /// (false) the login attempt was successful.
        /// <para>
        /// See also: <seealso cref="ErrorInformation"/>
        /// </para>
        /// </summary>
        /// <value>
        ///   <c>true</c> if [login successful]; otherwise, <c>false</c>.
        /// </value>
        public bool LoginSuccessful { get; set; }

        public int LoginFailureCode { get; set; }

        /// <summary>
        /// Gets or sets the error information.
        /// <para>
        /// If the <c>LoginSuccessful</c> property of this class is <c>false</c>
        /// this string should contain the error message to be displayed to
        /// the end-user.
        /// </para>
        /// </summary>
        public string ErrorInformation { get; set; }
    }
}
