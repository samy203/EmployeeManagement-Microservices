// Create user
dbAdmin = db.getSiblingDB("admin");
dbAdmin.createUser({
    user: "root",
    pwd: "password",
    roles: [{ role: "userAdminAnyDatabase", db: "admin" }],
    mechanisms: ["SCRAM-SHA-1"],
});

// Authenticate user
dbAdmin.auth({
    user: "root",
    pwd: "password",
    mechanisms: ["SCRAM-SHA-1"],
    digestPassword: true,
});

// Create DB and collection
db = new Mongo().getDB("OfficeManagement.Department");
db.createCollection("departments", { capped: false });