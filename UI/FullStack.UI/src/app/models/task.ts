export interface Task{
    id: string;
    name: string;
    description: string;
    dateOfCreation?: Date;
    isDone?: boolean;
    deadline?: Date;
}