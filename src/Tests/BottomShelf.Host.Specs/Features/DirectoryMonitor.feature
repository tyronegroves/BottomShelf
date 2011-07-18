Feature: DirectoryMonitor
	In order to monitor directories
	As the system
	I want to be notified when files and directories have been created, changed, or removed within a directory being monitored

Scenario: A directory is created in the directory being monitored
	Given the directory 'MyDirectory1' exists
	And I am monitoring the directory 'MyDirectory1'
	When the directory 'MyDirectory1\MyCreatedDirectory' is created
	Then the directory monitor should notify that the directory 'MyDirectory1\MyCreatedDirectory' was created
	
Scenario: A directory two levels deep is created in the directory being monitored
	Given the directory 'MyDirectory6\MyParentDirectory' exists
	And I am monitoring the directory 'MyDirectory6'
	When the directory 'MyDirectory6\MyParentDirectory\MyCreatedSubDirectory' is created
	Then the directory monitor should notify that the directory 'MyDirectory6\MyParentDirectory\MyCreatedSubDirectory' was created
	
Scenario: A directory in the directory being monitored is renamed
	Given the directory 'MyDirectory2\MyRenamedDirectory' exists
	And I am monitoring the directory 'MyDirectory2'
	When the directory 'MyDirectory2\MyRenamedDirectory' is renamed to 'MyDirectory2\MyRenamedDirectory2'
	Then the directory monitor should notify that the directory 'MyDirectory2\MyRenamedDirectory' was renamed to 'MyDirectory2\MyRenamedDirectory2'

Scenario: A directory two levels deep in the directory being monitored is renamed
	Given the directory 'MyDirectory5\MyParentDirectory\SubDirectory' exists
	And I am monitoring the directory 'MyDirectory5'
	When the directory 'MyDirectory5\MyParentDirectory\SubDirectory' is renamed to 'MyDirectory5\MyParentDirectory\SubRenamedDirectory'
	Then the directory monitor should notify that the directory 'MyDirectory5\MyParentDirectory\SubDirectory' was renamed to 'MyDirectory5\MyParentDirectory\SubRenamedDirectory'

Scenario: A directory is deleted in the directory being monitored
	Given the directory 'MyDirectory6\Deleted' exists
	And I am monitoring the directory 'MyDirectory6'
	When the directory 'MyDirectory6\Deleted' is deleted
	Then the directory monitor should notify that the directory 'MyDirectory6\Deleted' was deleted

Scenario: A file is created in the directory being monitored
	Given the directory 'MyDirectory3' exists
	And I am monitoring the directory 'MyDirectory3'
	When the file 'MyDirectory3\File1.txt' is created
	Then the directory monitor should notify that the file 'MyDirectory3\File1.txt' was created
	
Scenario: A file is created two levels deep in the directory being monitored
	Given the directory 'MyDirectory3' exists
	And I am monitoring the directory 'MyDirectory3'
	When the file 'MyDirectory3\ParentDirectory\File3.txt' is created
	Then the directory monitor should notify that the file 'MyDirectory3\ParentDirectory\File3.txt' was created

Scenario: A file is deleted from the directory being monitored
	Given the file 'MyDirectory4\File2.txt' exists
	And I am monitoring the directory 'MyDirectory4'
	When the file 'MyDirectory4\File2.txt' is deleted
	Then the directory monitor should notify that the file 'MyDirectory4\File2.txt' was deleted
	
Scenario: A file is deleted two levels deep from the directory being monitored
	Given the file 'MyDirectory4\ParentDirectory\File2.txt' exists
	And I am monitoring the directory 'MyDirectory4'
	When the file 'MyDirectory4\ParentDirectory\File2.txt' is deleted
	Then the directory monitor should notify that the file 'MyDirectory4\ParentDirectory\File2.txt' was deleted