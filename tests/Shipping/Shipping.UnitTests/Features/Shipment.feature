Feature: Shipment Feature

A short summary of the feature

@P8988000120_remains_created_state
Scenario: P8988000120 remains in Created state
	Given 'P8988000120' package in the database and created
	When I tried to deliver other packages and not 'P8988000120' package
	Then The state of the package 'P8988000120' should be Created
	
@Package_with_barcode_P8988000121_loaded_state
Scenario: Package with barcode P8988000121 remains loaded state
	Given 'P8988000121' package in the database
	When I tried to deliver 'P8988000121'
	Then The state of the package with the barcode 'P8988000121' should be Loaded
	
@Sack_with_barcode_C725800_unloaded_state
Scenario: Sack with barcode C725800 remains unloaded state
	Given 'C725800' sack in the database
	When I tried to deliver the sack'C725800'
	Then The state of the sack with the barcode 'C725800' should be unloaded

@Package_with_barcode_P8988000122_unloaded_state
Scenario: Package with barcode P8988000122 remains unloaded state
	Given 'P8988000122' package in the database for unloaded
	When I tried to deliver package 'P8988000122'
	Then The state of the package with the barcode 'P8988000122' should be unloaded

@Package_with_barcode_P8988000126_unloaded_state
Scenario: Package with barcode P8988000126 remains unloaded state
	Given 'P8988000126' pack in the database for unloaded
	When I tried to deliver the pack 'P8988000126' 
	Then The state of the pack with the barcode 'P8988000126' should be unloaded
