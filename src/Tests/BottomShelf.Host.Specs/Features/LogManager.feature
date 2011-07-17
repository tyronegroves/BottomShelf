Feature: LogManager
	In order to log
	As a user of the framework
	I want to be ability manage the log for different types

Scenario: Get the default log
	When I ask for the log using 'null' as the type
	Then the log should be of type 'BottomShelf.ConsoleLog, BottomShelf'

Scenario: Set the log factory method
	Given the log factory method will return a log of type 'BottomShelf.Host.Specs.Mocks.TestLogA, BottomShelf.Host.Specs' for 'BottomShelf.Host.Specs.Mocks.ClassA, BottomShelf.Host.Specs'
	And the log factory method will return a log of type 'BottomShelf.Host.Specs.Mocks.TestLogB, BottomShelf.Host.Specs' for 'BottomShelf.Host.Specs.Mocks.ClassB, BottomShelf.Host.Specs'
	When I ask for the log using 'BottomShelf.Host.Specs.Mocks.ClassA, BottomShelf.Host.Specs' as the type
	Then the log should be of type 'BottomShelf.Host.Specs.Mocks.TestLogA, BottomShelf.Host.Specs'
