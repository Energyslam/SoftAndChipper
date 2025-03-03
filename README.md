# SoftAndChipper

<h2>Design decisions:</h2>
<ul>
  <li>Patient, Document and DocumentRequest are used as both entity and domain class since they have no additional functions.</li>
  <li>Patient ID is the auto-increment of the table due to not knowing what would identify a patient uniquely in a real-world application (BSN? Composite key?). </li>
  <li>String is used for the request due to not knowing what can be requested in a real-world application (Limited set of request types?) </li>
  <li>A patient will be created if given patient ID does not exist in the database. Will create a new entry and use the created ID, not given patient ID. Do not know the real-life sequence of actions when a patient is referred to a different hospital (Always accompanied with files? Manually created beforehand?)  </li>
  <li>Files are stored in a folder in the project, would be a blob storage in an actual application. Should be abstracted so implementation does not care how files are stored.  </li>
</ul>

<h2>Inner workings:</h2>  
<b>RequestDocuments endpoint</b></br>
A database entry will be created for the request containing the string value of the requested information and the patient ID. Returns a 400 if the patient cannot be found (or created), returns a 500 for other exceptions, returns a 200 if nothing goes wrong.  
</br>
</br>
<b>UploadDocuments endpoint</b></br>
Multiple files can be uploaded and each will be stored with its filename, filepath, creation date and patient id. Filepaths are stored while going through the files so if any errors occurs, the transaction can be rolled back and all prior to error uploaded files can be deleted. If the ID of a DocumentRequest is provided, a relation will be created between the document and the request. Returns a 400 if the patient cannot be found (or created), or if the DocumentRequest cannot be found. Returns a 500 for other exceptions, returns a 200 if nothing goes wrong.
