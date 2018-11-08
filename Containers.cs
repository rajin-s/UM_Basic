/*
    UModules::Reference<T>

    by: Rajin Shankar
    part of: UM_Basic

    available to use according to UM_Basic/LICENSE
 */

 namespace UModules
 {
     /// <summary>
     /// Generic class for storing a single value in a referencable structure
     /// </summary>
     /// <typeparam name="T">Type of the value being stored</typeparam>
    /// <module>UM_Basic</module>
     public class Reference<T>
     {
         /// <summary>
         /// The stored value
         /// </summary>
         /// <access>private T</access>
         private T value = default(T);

         /// <summary>
         /// Set the stored value
         /// </summary>
         /// <access>public T</access>
         /// <param name="newValue">The new value to store</param>
         /// <returns>The new value</returns>
         public T Set(T newValue)
         {
             value = newValue;
             return value;
         }
         
         /// <summary>
         /// Implicitly convert a Reference object into its stored value
         /// </summary>
         /// <access>public static T</access>
         /// <param name="reference">The Reference object to convert</param>
         public static implicit operator T(Reference<T> reference)
         {
             return reference.value;
         }
     }
 }
 