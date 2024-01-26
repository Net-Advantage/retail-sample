# Shopper Account Registration Workflow

## Scope
Create a new valid shopper account in the Nabs Retail system. The shopper account is a prerequisite to place orders using the checkout scenario.

## Acceptance Criteria
- The shopper successfully initiates the account registration process.
- The shopper successfully submits the required information to create a new account.
- The shopper successfully activates the account.
- The shopper is able to login to the system using the new account.

### Shopper Account Registration Workflow State
State Properties:
- Id - Guid, required, generated
- Username - string, required, must be unique
- Email - string, required, may be duplicated
- ShopperType - enum, required, must be either Individual or Organisation
- Name - string, required for Organisation, generated for Individual
- FirstName - string, required for Individual
- LastName - string, required for Individual
- Telephone - string, required
- VerifiedOn - DateTime, required

### Initiate Shopper Account Registration Activity
- The shopper initiates the registration process.
- `Activity State Initialiser`
    - The system instantiates a new Registration Activity.
    - The system generates and Shopper Account Registration UI Manifest.
- The shopper is presented with a blank registration form prepopulated with sensible defaults.
- After a period of inactivity (configurable):
    - A New Registration Activity Discarded Event is raised.
    - The registration activity is discarded.

### Submit Shopper Account Registration Activity
- The shopper enters the required information to create a new account.
- The shopper submits the form.
- The system receives the Activity State.
- `Activity State Transformer`
- The `Activity State Validator` validates the Activity State:
    - username is unique.
    - password meets complexity requirements.
    - username is a valid email address by default.
    - email is valid.
    - telephone is valid.
    - name is valid.
    - In the case of an entity that has the same email as a person and they are both shoppers, the entity is created with a different username.
    - The shopper can be designated a person or an entity.
    - In the case of a person, the name is a concatination of the first name and last name.
    - In the case of an entity, the name is the entity name. No option to capture first and last name.
- If the information is invalid:
    - the shopper is presented with the `Shopper Account Registration` form with the invalid fields highlighted.
- If the information is valid:
    - the information is persisisted.
    - a `Shopper Account Created` event is raised.
    - the shopper is presented with a `Shopper Account Created` confirmation page.

### Shopper Account Created Event Handler Activity
- The system sends a confirmation email to the shopper.
- The shopper receives a confirmation email with a link to activate the account.

### Shopper Account Activation Activity
- The shopper clicks on the link in the confirmation email and:
    - account is activated.
    - `Shopper Account Activated` event is raised
- The shopper is presented with a `Shopper Account Activated` confirmation page.

### RegistrationStateInitialiser


### RegistrationStateValidator


## User Profile Management Scenario

## Store Management Scenario

## Shopping Cart Scenario

The outcome of this scenario is to create a valid shopping cart for a user. The shopping cart is a prerequisite for users place orders using the checkout scenario. Shopping carts can be created annonimously or for a specific user. Annonimous shopping carts are automatically convert to user shopping carts.

## Checkout Scenario